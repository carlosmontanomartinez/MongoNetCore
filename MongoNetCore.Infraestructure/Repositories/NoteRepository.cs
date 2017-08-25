using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoNetCore.Data;
using MongoNetCore.Data.Context;
using MongoNetCore.Data.Models;
using MongoNetCore.Infraestructure.Interfaces;

namespace MongoNetCore.Infraestructure.Repositories
{
	public class NoteRepository : INoteRepository
	{
		private readonly NoteContext _context = null;

		public NoteRepository(IOptions<Settings> settings)
		{
			_context = new NoteContext(settings);
		}

		public async Task<IEnumerable<Note>> GetAllNotes()
		{
			try
			{
				return await _context.Notes.Find(_ => true).ToListAsync();
			}
			catch (Exception ex)
			{
				// log or manage the exception
				throw ex;
			}
		}

		public async Task<Note> GetNote(string id)
		{
			var filter = Builders<Note>.Filter.Eq("_id", id);

			try
			{
				return await _context.Notes
								.Find(filter)
								.FirstOrDefaultAsync();
			}
			catch (Exception ex)
			{
				// log or manage the exception
				throw ex;
			}
		}

		public async Task AddNote(Note item)
		{
			try
			{
				await _context.Notes.InsertOneAsync(item);
			}
			catch (Exception ex)
			{
				// log or manage the exception
				throw ex;
			}
		}

		public async Task<DeleteResult> RemoveNote(string id)
		{
			try
			{
				return await _context.Notes.DeleteOneAsync(
					 Builders<Note>.Filter.Eq("_id", id));
			}
			catch (Exception ex)
			{
				// log or manage the exception
				throw ex;
			}
		}

        public async Task<UpdateResult> UpdateNote(ObjectId id, string body)
		{
			var filter = Builders<Note>.Filter.Eq(s => s._id, id);
			var update = Builders<Note>.Update
							.Set(s => s.body, body)
							.CurrentDate(s => s.updated_on);

			try
			{
				return await _context.Notes.UpdateOneAsync(filter, update);
			}
			catch (Exception ex)
			{
				// log or manage the exception
				throw ex;
			}
		}

		public async Task<ReplaceOneResult> UpdateNote(string id, Note item)
		{
			try
			{
				return await _context.Notes
							.ReplaceOneAsync(n => n._id.Equals(id)
											, item
											, new UpdateOptions { IsUpsert = true });
			}
			catch (Exception ex)
			{
				// log or manage the exception
				throw ex;
			}
		}

		// Demo function - full document update
		public async Task<ReplaceOneResult> UpdateNoteDocument(string id, string body)
		{
			var item = await GetNote(id) ?? new Note();
			item.body = body;
			item.updated_on = DateTime.Now;

			return await UpdateNote(id, item);
		}

		public async Task<DeleteResult> RemoveAllNotes()
		{
			try
			{
				return await _context.Notes.DeleteManyAsync(new BsonDocument());
			}
			catch (Exception ex)
			{
				// log or manage the exception
				throw ex;
			}
		}
	}
}

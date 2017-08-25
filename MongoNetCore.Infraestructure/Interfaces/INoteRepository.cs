using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoNetCore.Data;

namespace MongoNetCore.Infraestructure.Interfaces
{
	public interface INoteRepository
	{
		Task<IEnumerable<Note>> GetAllNotes();
		Task<Note> GetNote(string id);
		Task AddNote(Note item);
		Task<DeleteResult> RemoveNote(string id);
        Task<UpdateResult> UpdateNote(ObjectId id, string body);

		// demo interface - full document update
		Task<ReplaceOneResult> UpdateNoteDocument(string id, string body);

		// should be used with high cautious, only in relation with demo setup
		Task<DeleteResult> RemoveAllNotes();
	}
}

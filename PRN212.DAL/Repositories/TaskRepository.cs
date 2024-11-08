﻿using Microsoft.EntityFrameworkCore;
using PRN212.DAL.Models;

namespace PRN212.DAL.Repositories
{
    public class TaskRepository
    {
        private ToDoAppDbContext _context;

        public List<Models.Task> GetTasks()
        {
            _context = new ToDoAppDbContext();
            return _context.Tasks.ToList();
        }
        public List<Models.Task> GetTaskById(Guid id) {
            _context = new ToDoAppDbContext();
            return _context.Tasks.Include("User").Where(t=> t.UserId==id).ToList();
        }

        public void DeleteTask(Models.Task task)
        {
            _context.Tasks.Remove(task);
            _context.SaveChanges();
        }

        public DAL.Models.Task? Find(int id)
        {
            return _context.Tasks.FirstOrDefault(t => t.Id == id);
        }

    }
}

using System;
using System.Collections.Generic;
using MeuTodo.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MeuTodo.Repositories
{
    public interface ITarefaRepository
    {
        List<Tarefa> Read(Guid id);
        void Create(Tarefa terafa);
        void Delete(Guid id);
        void Update(Guid id, Tarefa tarefa);    
    }

    public class TarefaRepository : ITarefaRepositoriy
    {
        private readonly DataContext _context;

        public TarefaRepository(DataContext context)
        {
            _context = context;
        }
        public void Create(Tarefa terafa)
        {
            _context.Tarefas.Add(tarefa);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var tarefa = _context.Tarefas.Find(id);
            _context.Entry(tarefa).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public List<Tarefa> Read()
        {
            return _context.Tarefas.Where(tarefa => tarefa.UsuarioId == id).ToList(); //o .Where antes do check do id faz consumo de recursos
        }

        public void Update(Guid id, Tarefa tarefa)
        {
            var _tarefa = _context.Tarefas.Find(id);
            
            _tarefa.Nome = tarefa.Nome;
            _tarefa.Concluida = tarefa.Concluida;

            _context.Entry(_tarefa).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
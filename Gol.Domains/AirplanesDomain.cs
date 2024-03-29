﻿using Gol.Domains.Generics;
using Gol.Entities;
using Gol.Entities.Exceptions;
using Gol.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gol.Domains
{
    public class AirplanesDomain : IDomain<Airplane>
    {
        private GolContext _context;
        private readonly IConfiguration _configuration;

        public AirplanesDomain(IConfiguration configuration)
        {
            _configuration = configuration;

            using (_context = new GolContext(_configuration))
            {
                _context.Database.Migrate();
            }
        }

        /// <summary>
        /// Removes the specified entity from database.
        /// </summary>
        /// <param name="entity">Entity to be deleted.</param>
        /// <exception cref="AirplaneException">Throws when something goes worng. The InnerException can be used to have more details.</exception>
        /// <returns></returns>
        public async Task DeleteAsync(Airplane entity)
        {
            try
            {
                using (_context = new GolContext(_configuration))
                {
                    _context.Airplanes.Remove(entity);
                    var rowsAffected = await _context.SaveChangesAsync();
                }
            }
            catch(Exception e)
            {
                throw new AirplaneException("Não foi possível remover o avião informado.", e);
            }
        }

        /// <summary>
        /// Gets all entities available to database.
        /// </summary>
        /// <exception cref="AirplaneException">Throws when something goes worng. The InnerException can be used to have more details.</exception>
        /// <returns></returns>
        public async Task<IEnumerable<Airplane>> GetAsync()
        {
            try
            {
                var airplanes = default(IEnumerable<Airplane>);
                using (_context = new GolContext(_configuration))
                {
                    airplanes = await _context.Airplanes.ToListAsync();
                }

                return airplanes;
            }
            catch(Exception e)
            {
                throw new AirplaneException("Não foi possível buscar os aviões disponíveis.", e);
            }
        }

        /// <summary>
        /// Gets a specific entity based on its unique identifier.
        /// </summary>
        /// <exception cref="AirplaneException">Throws when something goes worng. The InnerException can be used to have more details.</exception>
        /// <returns></returns>
        public async Task<Airplane> GetByIdAsync(int id)
        {
            try
            {
                var airplane = default(Airplane);
                using (_context = new GolContext(_configuration))
                {
                    airplane = await _context.Airplanes.FirstOrDefaultAsync(a => a.ID.Equals(id));
                }

                return airplane;
            }
            catch(Exception e)
            {
                throw new AirplaneException("Não foi possível buscar o avião solicitado.", e);
            }
        }

        /// <summary>
        /// Saves a new entity to database.
        /// </summary>
        /// <param name="entity">The entity to be saved.</param>
        /// <exception cref="AirplaneException">Throws when something goes worng. The InnerException can be used to have more details.</exception>
        /// <returns></returns>
        public async Task<Airplane> SaveAsync(Airplane entity)
        {
            try
            {
                var airplane = default(Airplane);
                using (_context = new GolContext(_configuration))
                {
                    var entry = await _context.AddAsync(entity);
                    var rowsAffected = await _context.SaveChangesAsync();

                    if(rowsAffected > 0)
                    {
                        airplane = entry.Entity;
                    }
                }

                if (airplane == null)
                {
                    throw new AirplaneException("O avião não foi salvo na base de dados.");
                }

                return airplane;
            }
            catch(AirplaneException e)
            {
                throw e;
            }
            catch(Exception e)
            {
                throw new AirplaneException("Não foi possível salvar o avião informado.", e);
            }
        }

        /// <summary>
        /// Updates the entity, based on its unique identifier.
        /// </summary>
        /// <param name="entity">Entity to be updated.</param>
        /// <exception cref="AirplaneException">Throws when something goes worng. The InnerException can be used to have more details.</exception>
        /// <returns></returns>
        public async Task<Airplane> UpdateAsync(Airplane entity)
        {
            try
            {
                var airplane = default(Airplane);
                using (_context = new GolContext(_configuration))
                {
                    var entry =_context.Airplanes.Update(entity);
                    var rowsAffected = await _context.SaveChangesAsync();

                    if (rowsAffected > 0)
                    {
                        airplane = entry.Entity;
                    }
                }

                if (airplane == null)
                {
                    throw new AirplaneException("O avião não foi atualizado na base de dados.");
                }

                return airplane;
            }
            catch(Exception e)
            {
                throw new AirplaneException("Não foi possível atualizar o avião informado.", e);
            }
        }
    }
}

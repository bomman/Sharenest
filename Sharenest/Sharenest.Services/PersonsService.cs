using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using Sharenest.Data.Interfaces;
using Sharenest.Models.EntityModels;
using Sharenest.Models.ViewModels.Account;
using Sharenest.Services.Interfaces;

namespace Sharenest.Services
{
    public class PersonsService : Service, IPersonsService
    {
        private readonly IPersonsRepository personsRepository;

        public PersonsService(IPersonsRepository personsRepository) : base()
        {
            this.personsRepository = personsRepository;
        }


        public void AddPerson(RegisterViewModel model, ApplicationUser user)
        {
            Person newPerson = AutoMapper.Mapper.Map<RegisterViewModel, Person>(model);
            newPerson.UserId = user.Id;
            personsRepository.InsertOrUpdate(newPerson);

            try
            {
                personsRepository.Commit();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
        }
    }
}

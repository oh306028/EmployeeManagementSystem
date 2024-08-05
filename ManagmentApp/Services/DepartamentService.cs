﻿using AutoMapper;
using ManagmentApp.Dtos;
using ManagmentApp.Models;
using ManagmentApp.Repositories;

namespace ManagmentApp.Services
{
    public interface IDepartamentService
    {
        Task CreateDepartamentAsync(CreateDepartamentDto dto);   

    }   
    public class DepartamentService : IDepartamentService
    {
        private readonly DepartamentRepo _departamentRepo;
        private readonly IMapper _mapper;

        public DepartamentService(DepartamentRepo departamentRepo, IMapper mapper)
        {
            _departamentRepo = departamentRepo;
            _mapper = mapper;
        }
        public async Task CreateDepartamentAsync(CreateDepartamentDto dto)
        {
            var result = _mapper.Map<Departament>(dto);

            await _departamentRepo.CreateAsync(result); 
        }
    }
}
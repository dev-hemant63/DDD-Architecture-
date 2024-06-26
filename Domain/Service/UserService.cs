﻿using Domain.Entities;
using Domain.Interface;
using Infrastructure;
using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Domain.Service
{
    public class UserService : IUserService
    {
        private readonly IContext _context;
        public UserService(IContext context)
        {
            _context = context;
        }
        public async Task<Response> Create(ApplicationUsers users)
        {
            var response = new Response
            {
                ResponseText = "An error has occured try after sometime.",
                StatusCode = ResponseStatus.Failed
            };
            try
            {
                response = await _context.GetAsync<Response>("Proc_AddUser",new
                {
                    users.FirstName,
                }, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                response.ResponseText = "Error in creating user.";
            }
            return response;
        }
        public async Task<Response<IEnumerable<ApplicationUsers>>> List(int loginId)
        {
            var response = new Response<IEnumerable<ApplicationUsers>>
            {
                ResponseText = "An error has occured try after sometime.",
                StatusCode = ResponseStatus.Failed
            };
            try
            {
                response.Result = await _context.GetAllAsync<ApplicationUsers>("Proc_GetUsers", new
                {
                    loginId,
                }, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                response.ResponseText = "Error in creating user.";
            }
            return response;
        }
        public async Task<Response<ApplicationUsers>> GetById(int loginId)
        {
            var response = new Response<ApplicationUsers>
            {
                ResponseText = "An error has occured try after sometime.",
                StatusCode = ResponseStatus.Failed
            };
            try
            {
                response.Result = await _context.GetAsync<ApplicationUsers>("Proc_GetUserById", new
                {
                    loginId,
                }, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                response.ResponseText = "Error in creating user.";
            }
            return response;
        }
        public async Task<Response> DeleteUser(int Id)
        {
            var response = new Response
            {
                ResponseText = "An error has occured try after sometime.",
                StatusCode = ResponseStatus.Failed
            };
            try
            {
                response = await _context.GetAsync<Response>("Proc_DeleteUser", new
                {
                    Id
                }, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                response.ResponseText = "Error in creating user.";
            }
            return response;
        }
    }
}

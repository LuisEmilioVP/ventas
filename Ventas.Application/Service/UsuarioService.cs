﻿using System;
using Microsoft.Extensions.Logging;
using Ventas.Application.Contract;
using Ventas.Application.Core;
using Ventas.Application.Dtos.Usuario;
using Ventas.Application.Extentions;
using Ventas.Application.Validations;
using Ventas.Infrastructure.Exceptions;
using Ventas.Infrastructure.Interfaces;

namespace Ventas.Application.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository usuarioRepository;
        private readonly ILogger<UsuarioService> logger;

        public UsuarioService(IUsuarioRepository usuarioRepository, 
                              ILogger<UsuarioService> logger)
        {
            this.usuarioRepository = usuarioRepository;
            this.logger = logger;
        }

        public ServiceResult Get()
        {
            ServiceResult result = new ServiceResult();

            try
            {
                var users = this.usuarioRepository.GetAllUser();
                result.Data = users;
                result.Message = "Usuarios obtenidos exitosamente";
            }
            catch (UsuarioExceptions uex)
            {
                result.Success = false;
                result.Message = uex.Message;
                this.logger.LogError($"Algo salió mal {result.Message}");
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error obteniendo los usuarios";
                this.logger.LogError($"{result.Message}", ex.ToString());
            }

            return result;
        }

        public ServiceResult GetById(int id)
        {
            ServiceResult result = UsuarioValidator.ValidateIdUsuario(id);

            if (!result.Success)
            {
                return result;
            }

            try
            {
                var user = this.usuarioRepository.GetUserById(id);
                result.Data = user;
                result.Message = $"Usuario obtenido exitosamente {id}";
            }
            catch (UsuarioExceptions uex)
            {
                result.Success = false;
                result.Message = uex.Message;
                this.logger.LogError($"Algo salió mal {result.Message}");
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error al obtener usuario";
                this.logger.LogError($"{result.Message}", ex.ToString());
            }

            return result;
        }

        public ServiceResult Save(UsuarioAddDto model)
        {
            ServiceResult result = UsuarioValidator.ValidateUsuarioAdd(model);

            if (!result.Success)
            {
                return result;
            }

            try
            {
                var usuario = model.ConvertDtoAddToEntity();
                this.usuarioRepository.Add(usuario);

                result.Message = "Usuario agregado correctamente";
            }
            catch (UsuarioExceptions uex)
            {
                result.Success = false;
                result.Message = uex.Message;
                this.logger.LogError($"Algo salió mal {result.Message}");
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error guardando el usuario";
                this.logger.LogError($"{result.Message}", ex.ToString());
            }

            return result;
        }

        public ServiceResult Update(UsuarioUpdateDto model)
        {
            ServiceResult result = UsuarioValidator.ValidateUsuarioUpdate(model);

            if (!result.Success)
            {
                return result;
            }

            try
            {
                var usuario = model.ConvertDtoUpdateToEntity();
                this.usuarioRepository.Update(usuario);

                result.Message = "Usuario actualizado correctamente";
            }
            catch (UsuarioExceptions uex)
            {
                result.Success = false;
                result.Message = uex.Message;
                this.logger.LogError($"Algo salió mal {result.Message}");
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error actualizando el usuario";
                this.logger.LogError($"{result.Message}", ex.ToString());
            }

            return result;
        }

        public ServiceResult Remove(UsuarioRevoveDto model)
        {
            ServiceResult result = UsuarioValidator.ValidateUsuarioRemove(model);

            try
            {
               var usuario = model.ConvertRemoveDtoToEntity();
                this.usuarioRepository.Remove(usuario);

                result.Message = "Usuario eliminado correctamente";
            }
            catch (UsuarioExceptions uex)
            {
                result.Success = false;
                result.Message = uex.Message;
                this.logger.LogError($"Algo salió mal {result.Message}");
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error al intentar eliminar usuario";
                this.logger.LogError($"{result.Message}", ex.ToString());
            }

            return result;
        }
    }
}

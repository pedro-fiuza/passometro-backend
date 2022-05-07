﻿using System.ComponentModel.DataAnnotations;

namespace Domain.Dto
{
    public class RegisterDto
    {
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Senha é obrigatório."), StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;
        [Compare("Password", ErrorMessage = "As senhas nao combinam.")]
        [Required(ErrorMessage = "É obrigatório confirmar a senha.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}

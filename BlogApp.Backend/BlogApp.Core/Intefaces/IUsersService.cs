﻿using BlogApp.Models.InputModels;

namespace BlogApp.Core.Intefaces;

public interface IUsersService
{
    void Add(User userModel);
}
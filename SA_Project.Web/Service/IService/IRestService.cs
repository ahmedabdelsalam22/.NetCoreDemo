﻿namespace SA_Project.Web.Service.IService
{
    public interface IRestService<T> where T : class
    {
        Task<T> UpdateAsync(string url, T data, bool withBearer = true);
        Task PostAsync(string url, T data, bool withBearer = true);
        Task<List<T>> GetAsync(string url, bool withBearer = true);
        Task<T> GetByIdAsync(string url, bool withBearer = true);
        Task Delete(string url, bool withBearer = true);
    }
}

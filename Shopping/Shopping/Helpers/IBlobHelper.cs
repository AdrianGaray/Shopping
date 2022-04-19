namespace Shopping.Helpers
{
    public interface IBlobHelper
    {
        /// <summary>
        /// IFormFile: cuando se selecciona una archivo
        /// </summary>
        /// <param name="file"></param>
        /// <param name="containerName"></param>
        /// <returns></returns>
        Task<Guid> UploadBlobAsync(IFormFile file, string containerName);

        /// <summary>
        /// Sirve para programacion distribuida (aplicaciones mobiles). Cuando se toma foto desde el telefono
        /// </summary>
        /// <param name="file"></param>
        /// <param name="containerName"></param>
        /// <returns></returns>
        Task<Guid> UploadBlobAsync(byte[] file, string containerName);
        /// <summary>
        /// image: Ruta de una archivo(fisica) de la maquina
        /// </summary>
        /// <param name="image"></param>
        /// <param name="containerName"></param>
        /// <returns></returns>
        Task<Guid> UploadBlobAsync(string image, string containerName);

        Task DeleteBlobAsync(Guid id, string containerName);
    }
}

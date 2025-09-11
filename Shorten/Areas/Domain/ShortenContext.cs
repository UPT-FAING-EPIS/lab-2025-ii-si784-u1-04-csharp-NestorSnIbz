using Microsoft.EntityFrameworkCore;
using Shorten.Areas.Domain; // ✅ O el namespace correcto donde esté UrlMapping

namespace Shorten.Areas.Domain
{
    /// <summary>
    /// Clase de infraestructura que representa el contexto de la base de datos
    /// </summary>
    public class ShortenContext : DbContext
    {
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="options">Opciones de conexión de BD</param>
        public ShortenContext(DbContextOptions<ShortenContext> options) : base(options)
        {
        }

        /// <summary>
        /// Propiedad que representa la tabla de mapeo de URLs
        /// </summary>
        /// <value>Conjunto de UrlMapping</value>
        public DbSet<UrlMapping> UrlMappings { get; set; } = null!;
    }
}

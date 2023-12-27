using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;

namespace Api;

public interface IProductData
{
    Task<Post> AddProduct(Post product);
    Task<bool> DeleteProduct(int id);
    Task<IEnumerable<Post>> GetProducts();
    Task<Post> UpdateProduct(Post product);
}

public class ProductData : IProductData
{
    private readonly List<Post> products = new List<Post>
        {
            new Post
            {
                Id = 1,
                Name= "SIHST",
                Description= "Pagina Web de Seguridad e Higiene",
                Url="https://sihst-638fd.firebaseapp.com/",
                Image="data/images/sihst.png"
            },
            new Post
            {

                Id= 2,
                Name= "Agrimensor Iraguen",
                Description= "Pagina Web del Agrimensor Nicolas Iraguen",
                Url= "https://agrimensoriraguen.web.app/",
                Image= "data/images/agrimensoriraguen.png"
            },
            new Post
            {
                Id= 3,
                Name= "Estudio E Gutierrez",
                Description= "Pagina Web del Estudio E Gutierrez",
                Url= "https://estudio-e-gutierrez.web.app/",
                Image= "data/images/estudioegutierrez.png"
            },
            new Post
            {
                Id= 3,
                Name= "Android Apps",
                Description= "Applicaciones Android",
                Url= "https://play.google.com/store/search?q=puelogames&c=apps&hl=es_AR&gl=US",
                Image= "data/images/androidapps.png"
            },
             new Post
            {
                Id= 4,
                Name= "GitHub",
                Description= "Mi Github",
                Url= "https://github.com/lisandro-iraguen",
                Image= "data/images/mygithub.png"
            }
        };

    public Task<Post> AddProduct(Post product)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteProduct(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Post>> GetProducts()
    {
        return Task.FromResult(products.AsEnumerable());
    }

    public Task<Post> UpdateProduct(Post product)
    {
        throw new NotImplementedException();
    }
}

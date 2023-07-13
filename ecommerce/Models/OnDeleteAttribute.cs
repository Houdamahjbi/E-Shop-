using Microsoft.EntityFrameworkCore;

namespace ecommerce.Models
{
    internal class OnDeleteAttribute : Attribute
    {
        private DeleteBehavior noAction;

        public OnDeleteAttribute(DeleteBehavior noAction)
        {
            this.noAction = noAction;
        }
    }
}
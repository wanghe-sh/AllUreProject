using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Data
{
    static class Extensions
    {
        public static PrimitivePropertyConfiguration IsIndex(this PrimitivePropertyConfiguration config, bool unique = false)
        {
            return config.HasColumnAnnotation(
                IndexAnnotation.AnnotationName, 
                new IndexAnnotation(new IndexAttribute { IsUnique = unique }));
        }
    }
}

using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.Web.Models
{
    public class MainViewModel
    {
        public CategoryOutput[] Categories { get; set; }
        public Language[] Languages { get; set; }

        public User User { get; set; }

        public bool IsSimpleModel { get; set; }

        public MainViewModel()
        {

        }

    }

    public class MainViewModel<T> : MainViewModel
    {
        public T BodyModel { get; private set; }

        public MainViewModel(T bodyModel)
        {
            this.BodyModel = bodyModel;
        }
    }
}
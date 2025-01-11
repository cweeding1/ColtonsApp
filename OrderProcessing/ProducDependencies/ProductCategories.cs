using System.ComponentModel;

namespace OrderProcessing.ProducDependencies
{
    enum ProductCategories
    {
        [Description("cat1")]
        category1 = 1,

        [Description("cat2")]
        category2 = 2,
        
        [Description("cat3")]
        category3 = 3,
    }
}

using System.Windows;
using System.Windows.Controls;

namespace LIST_To_Do
{
    public class PageTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TaskTemplate { get; set; }
        public DataTemplate BookTemplate { get; set; }
        public DataTemplate IdeaTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is string page)
            {
                switch (page)
                {
                    case "Tasks":
                        return TaskTemplate;
                    case "Books":
                        return BookTemplate;
                    case "Ideas":
                        return IdeaTemplate;
                }
            }

            return base.SelectTemplate(item, container);
        }
    }
}

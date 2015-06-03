using Orders.com.BLL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Facile.Core.Extensions;

namespace Orders.com.WPF.VM
{
    public class CategoriesVM : ViewModelBase
    {
        private CategoryService _categoriesService;
        private ObservableCollection<CategoryVM> _categories;
        private ICommand _addCategoryCommand;
        private ICommand _saveCategoriesCommand;
        private ICommand _loadCategoriesCommand;
        private ICommand _deleteSelectedCommand;

        public CategoriesVM(CategoryService categoryService)
        {
            _categoriesService = categoryService;
            _addCategoryCommand = new Command(() => AddCategory());
            _saveCategoriesCommand = new Command(() => SaveCategories());
            _loadCategoriesCommand = new Command(() => LoadCategories());
            _deleteSelectedCommand = new Command(() => DeleteSelectedVM());
        }

        public CategoryVM SelectedCategory
        {
            get;
            set;
        }

        public ICommand AddCategoryCommand
        {
            get { return _addCategoryCommand; }
        }
        
        public ICommand SaveCategoriesCommand
        {
            get { return _saveCategoriesCommand; }
        }

        public ICommand LoadCategoriesCommand
        {
            get { return _loadCategoriesCommand; }
        }

        public ICommand DeleteSelectedCommand
        {
            get { return _deleteSelectedCommand; }
        }
        
        public IEnumerable<CategoryVM> Categories
        {
            get { return _categories; }
            set
            {
                _categories = new ObservableCollection<CategoryVM>(value);
                OnPropertyChanged("Categories");
            }
        }

        private async Task LoadCategories()
        {
            var result = await _categoriesService.GetAllCommand().ExecuteAsync();
            var vms = result.Value.Select(c => new CategoryVM(c, _categoriesService));
            Categories = vms.ToArray();
        }

        private async Task SaveCategories()
        {
            var results = Categories.Select(vm => vm.SaveAsync()).ToArray();
            await Task.WhenAll(results);
        }

        private void AddCategory()
        {
            _categories.Add(new CategoryVM(_categoriesService));
        }

        private async Task DeleteSelectedVM()
        {
            if (SelectedCategory != null && !SelectedCategory.IsNew)
            {
                await _categoriesService.DeleteCommand(SelectedCategory.ID).ExecuteAsync();
                _categories.Remove(SelectedCategory);
                SelectedCategory = null;
            }
        }
    }
}

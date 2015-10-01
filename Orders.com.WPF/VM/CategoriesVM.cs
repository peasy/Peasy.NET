using Peasy.Core.Extensions;
using Orders.com.BLL;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Orders.com.WPF.VM
{
    public class CategoriesVM : ViewModelBase
    {
        private CategoryService _categoriesService;
        private ObservableCollection<CategoryVM> _categories = new ObservableCollection<CategoryVM>();
        private ICommand _addCategoryCommand;
        private ICommand _saveCategoriesCommand;
        private ICommand _loadCategoriesCommand;
        private ICommand _deleteSelectedCommand;

        public CategoriesVM(CategoryService categoryService)
        {
            _categoriesService = categoryService;
            _addCategoryCommand = new Command(() => AddCategory());
            _saveCategoriesCommand = new Command(async () => await SaveCategoriesAsync());
            _loadCategoriesCommand = new Command(async () => await LoadCategoriesAsync());
            _deleteSelectedCommand = new Command(async () => await DeleteSelectedItemAsync());
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
        }

        private async Task LoadCategoriesAsync()
        {
            var result = await _categoriesService.GetAllCommand().ExecuteAsync();
            _categories.Clear();
            var vms = result.Value.Select(c => new CategoryVM(c, _categoriesService))
                                  .ForEach(vm =>
                                  {
                                      SubscribeHandlers(vm);
                                      _categories.Add(vm);
                                  });
        }

        private async Task SaveCategoriesAsync()
        {
            var results = Categories.Select(vm => vm.SaveAsync()).ToArray();
            await Task.WhenAll(results);
        }

        private void AddCategory()
        {
            var category = new CategoryVM(_categoriesService);
            SubscribeHandlers(category);
            _categories.Add(category);
        }

        private void SubscribeHandlers(CategoryVM category)
        {
            category.EntityDeleted += (s, e) => _categories.Remove(SelectedCategory);
        }

        private async Task DeleteSelectedItemAsync()
        {
            if (SelectedCategory.IsNew)
                _categories.Remove(SelectedCategory);
            else
                await SelectedCategory.DeleteAsync(); 
        }
    }
}

using System.Collections.ObjectModel;

namespace ShopShowcase.ViewModels;

public class OptionViewModel(string name, IEnumerable<string> values, string initial) : BaseViewModel
{
    public string Name { get; } = name;

    public ObservableCollection<string> Values { get; } = new ObservableCollection<string>(values);

    private string _selectedValue = initial;
    public string SelectedValue
    {
        get => _selectedValue;
        set
        {
            if (SetProperty(ref _selectedValue, value))
            {
                SelectedChanged?.Invoke(this, new SelectedOptionChangedEventArgs(Name, value));
            }
        }
    }

    public event EventHandler<SelectedOptionChangedEventArgs>? SelectedChanged;
}

public class SelectedOptionChangedEventArgs(string name, string value) : EventArgs
{
    public string Name { get; } = name;
    public string Value { get; } = value;
}

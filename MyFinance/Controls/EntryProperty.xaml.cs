namespace MyFinance.Controls;

public partial class EntryProperty : ContentView
{
	public EntryProperty()
	{
		InitializeComponent();
	}


	private static readonly BindableProperty NamePropertyProperty = BindableProperty.Create(nameof(NameProperty), typeof(string), typeof(EntryProperty),
		propertyChanged: OnNameChanged);
    private static readonly BindableProperty ValuePropertyProperty = BindableProperty.Create(nameof(ValueProperty), typeof(string), typeof(EntryProperty));


	public string NameProperty
	{
		get => (string)GetValue(NamePropertyProperty);
		set => SetValue(NamePropertyProperty, value);
	}
    public string ValueProperty
    {
        get => (string)GetValue(ValuePropertyProperty);
        set => SetValue(ValuePropertyProperty, value);
    }


	private static void OnNameChanged(BindableObject bindable, object oldValue, object newValue)
	{
		if (bindable is not EntryProperty p) return;
		p.Name.Text = newValue as string;
	}


    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
		ValueProperty = e.NewTextValue;
    }
}
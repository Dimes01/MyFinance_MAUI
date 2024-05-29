namespace MyFinance.Controls;

public partial class PickerProperty : ContentView
{
	public PickerProperty()
	{
		InitializeComponent();
	}

    private static readonly BindableProperty NamePropertyProperty = BindableProperty.Create(nameof(NameProperty), typeof(string), typeof(PickerProperty),
        propertyChanged: OnNameChanged);
    private static readonly BindableProperty ValuePropertyProperty = BindableProperty.Create(nameof(ValueProperty), typeof(string), typeof(PickerProperty),
        propertyChanged: OnValueChanged);


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
        if (bindable is not PickerProperty p) return;
        p.Name.Text = newValue as string;
    }
    private static void OnValueChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not PickerProperty p) return;
        if (newValue is not string str) return;
        p.Value.SelectedItem = p.Value.Items.FirstOrDefault(p => p == str);
    }

    private void Value_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (sender is not Picker picker) return;
        ValueProperty = picker.SelectedItem as string;
    }
}
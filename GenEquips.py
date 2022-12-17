   
def main():
    for x in range(0, 256):
        print('<DataGridTemplateColumn Width="Auto" Header="E[' + str(x) + ']" IsReadOnly="True"> <DataGridTemplateColumn.CellTemplate> <DataTemplate> <TextBlock Margin="4,0,4,0" Background="{StaticResource GESColorPanel}" Foreground="{StaticResource GESColorWhite}" Text="{Binding Path=EquipmentSlotList[' + str(x) + '].AsString}" /> </DataTemplate> </DataGridTemplateColumn.CellTemplate> </DataGridTemplateColumn>')
if __name__ == '__main__':
    main()
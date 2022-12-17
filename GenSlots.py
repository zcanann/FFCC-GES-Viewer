


                
def main():
    for x in range(0, 64):
        print('<DataGridTemplateColumn Width="Auto" Header="I[' + str(x) + ']" IsReadOnly="True"> <DataGridTemplateColumn.CellTemplate> <DataTemplate> <TextBlock Margin="4,0,4,0" Background="{StaticResource GESColorPanel}" Foreground="{StaticResource GESColorWhite}" Text="{Binding Path=Items[' + str(x) + ']}" /> </DataTemplate> </DataGridTemplateColumn.CellTemplate> </DataGridTemplateColumn>')
    for x in range(0, 96):
        print('<DataGridTemplateColumn Width="Auto" Header="A[' + str(64 + x) + ']" IsReadOnly="True"> <DataGridTemplateColumn.CellTemplate> <DataTemplate> <TextBlock Margin="4,0,4,0" Background="{StaticResource GESColorPanel}" Foreground="{StaticResource GESColorWhite}" Text="{Binding Path=Artifacts[' + str(x) + ']}" /> </DataTemplate> </DataGridTemplateColumn.CellTemplate> </DataGridTemplateColumn>')
if __name__ == '__main__':
    main()
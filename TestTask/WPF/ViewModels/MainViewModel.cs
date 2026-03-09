using System.Collections.ObjectModel;
using System.Windows.Input;
using Core.Models;
using Core.Services;
using Microsoft.Win32;
using WPF.Commands;

namespace WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<WellSummary> Wells { get; } = new();
        public ObservableCollection<string> Errors { get; } = new();
        public ICommand LoadCsvCommand { get; }
        public ICommand ExportJsonCommand {  get; }

        public MainViewModel()
        {
            LoadCsvCommand = new RelayCommand(LoadCsv);
            ExportJsonCommand = new RelayCommand(ExportJson);
        }

        private void LoadCsv(object? obj)
        {
            var dialog = new OpenFileDialog();

            if(dialog.ShowDialog() != true)
            {
                return;
            }

            var loader = new CsvLoader();

            var (wells, loadErrors) = loader.Load(dialog.FileName);

            var validator = new WellValidator();
            var validationErrors = validator.Validate(wells);

            var statService = new WellStatistics();

            Wells.Clear();

            foreach(var well in wells.Select(statService.Calculate))
            {
                Wells.Add(well);
            } 

            Errors.Clear();

            foreach(var error in loadErrors.Concat(validationErrors))
            {
                Errors.Add($"{error.RowNumber} | {error.WellId} | {error.ErrorMessage}");
            }
        }

        private void ExportJson(object? obj)
        {
            if (!Wells.Any())
            {
                return;
            }

            if(Errors.Any())
            {
                System.Windows.MessageBox.Show("Невозможно экспортировать данные, т.к. присутствуют ошибки в данных.", "Ошибка экспорта", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                return;
            }

            var dialog = new SaveFileDialog();
            dialog.Filter = "JSON file (*.json)|*.json";

            if(dialog.ShowDialog() != true)
            {
                return;
            }

            var exporter = new JsonExport();
            exporter.ExportData(Wells.ToList(), dialog.FileName);
        }
    }
}

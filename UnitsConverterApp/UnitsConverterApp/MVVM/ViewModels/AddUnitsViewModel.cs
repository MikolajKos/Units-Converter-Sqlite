using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using UnitsConverterApp.Core;
using UnitsConverterApp.MVVM.Models.DataModels.Entities;
using UnitsConverterApp.MVVM.Repositories;
using UnitsConverterApp.Validation;
using UnitsConverterApp.Validation.TypesOfValidation;
using static System.Windows.Visibility;

namespace UnitsConverterApp.MVVM.ViewModels
{
    public class AddUnitsViewModel : ObservableObject
    {
        private ContextRepo crep = new ContextRepo();


        private string _unitTypeInput;
        public string UnitTypeInput
        {
            get
            {
                return _unitTypeInput;
            }
            set
            {
                _unitTypeInput = value;
                OnPropertyChanged(nameof(UnitTypeInput));
            }
        }


        private string _unitName;
        public string UnitName
        {
            get
            {
                return _unitName;
            }
            set
            {
                _unitName = value;
                OnPropertyChanged(nameof(UnitName));
            }
        }


        private string _unitSymbol;
        public string UnitSymbol
        {
            get
            {
                return _unitSymbol;
            }
            set
            {
                _unitSymbol = value;
                OnPropertyChanged(nameof(UnitSymbol));
            }
        }


        private string _convertFromUnitSymbol;
        public string ConvertFromUnitSymbol
        {
            get => _convertFromUnitSymbol; 
            set 
            { 
                _convertFromUnitSymbol = value;
                OnPropertyChanged(nameof(ConvertFromUnitSymbol));
            }
        }


        private string _convertToUnitSymbol;
        public string ConvertToUnitSymbol
        {
            get => _convertToUnitSymbol;
            set
            {
                _convertToUnitSymbol = value;
                OnPropertyChanged(nameof(ConvertToUnitSymbol));
            }
        }


        private string _unitRatio;
        public string UnitRatio
        {
            get
            {
                return _unitRatio;
            }
            set
            {
                _unitRatio = value;
                OnPropertyChanged(nameof(UnitRatio));
            }
        }

        private List<string> _unitTypeList;
        public List<string> UnitTypeList
        {
            get => crep.GetUnitTypeList();
            set
            {
                _unitTypeList = crep.GetUnitTypeList();
                OnPropertyChanged(nameof(UnitTypeList));
            }
        }

        private List<string> _unitList;
        public List<string> UnitList
        {
            get => crep.GetUnitList(SelectedUnitType);
            set
            {
                _unitList = value;
                OnPropertyChanged(nameof(UnitList));
            }
        }


        private int _selectedUnitType;
        public int SelectedUnitType
        {
            get
            {
                return _selectedUnitType;
            }
            set
            {
                _selectedUnitType = value;
                OnPropertyChanged(nameof(SelectedUnitType), nameof(UnitList));
            }
        }


        private List<UnitType> _unitsTypeDataGridSource;
        public List<UnitType> UnitsTypeDataGridSource
        {
            get => crep.FillUnitsTypesDataGrid(); 
            set 
            { 
                _unitsTypeDataGridSource = value;
                OnPropertyChanged(nameof(UnitsTypeDataGridSource));
            }
        }



        private List<Unit> _unitsDataGridSource;
        public List<Unit> UnitsDataGridSource
        {
            get
            {
                return _unitsDataGridSource;
            }
            set
            {
                _unitsDataGridSource = value;
                OnPropertyChanged(nameof(UnitsDataGridSource));
            }
        }


        private IList _dataGridSelectedItems = new ArrayList();
        public IList DataGridSelectedItems
        {
            get => _dataGridSelectedItems;
            set
            {
                _dataGridSelectedItems = value;
                OnPropertyChanged(nameof(DataGridSelectedItems));
            }
        }


        private string _convertFromUnit;
        public string ConvertFromUnit
        {
            get => _convertFromUnit;
            set
            {
                //refreshing property
                ConvertFromUnitSymbol = crep.GetUnitSymbol(value);

                _convertFromUnit = value /*+ $" ({crep.GetUnitSymbol(value)})"*/;
                OnPropertyChanged(nameof(ConvertFromUnit));
            }
        }


        private string _convertToUnit;
        public string ConvertToUnit
        {
            get => _convertToUnit;
            set
            {
                //refreshing property
                ConvertToUnitSymbol = crep.GetUnitSymbol(value);

                _convertToUnit = value /*+ $" ({crep.GetUnitSymbol(value)})"*/;
                OnPropertyChanged(nameof(ConvertToUnit));
            }
        }


        private string _valueToConvert;
        public string ValueToConvert
        {
            get { return _valueToConvert; }
            set
            {
                _valueToConvert = value;
                CalculatedResult = null;
                OnPropertyChanged(nameof(ValueToConvert));
            }
        }


        private string _calculatedResult;
        public string CalculatedResult
        {
            get { return _calculatedResult; }
            set
            {
                _calculatedResult = crep.CalculateValue(ValueToConvert, ConvertFromUnit, ConvertToUnit);
                OnPropertyChanged(nameof(CalculatedResult));
            }
        }


        private string _errorMessages;
        public string ErrorMessages
        {
            get => _errorMessages;
            set
            {
                _errorMessages = value;
                OnPropertyChanged(nameof(ErrorMessages));
            }
        }


        private Visibility _errorVisibility = Hidden;
        public Visibility ErrorVisibility
        {
            get => _errorVisibility;
            set
            {
                _errorVisibility = value;
                OnPropertyChanged(nameof(ErrorVisibility));
            }
        }




        #region Commands

        private ICommand _addUnitType;

        public ICommand AddUnitTypeCommand
        {
            get
            {
                if (_addUnitType == null) _addUnitType = new RelayCommand(
                    (object o) =>
                    {
                        crep.AddUnitType(UnitTypeInput);
                        UnitTypeInput = string.Empty;

                        //Updates UnitType Datagrid
                        UnitsTypeDataGridSource = crep.FillUnitsTypesDataGrid();

                        //Updates unit type 
                        UnitTypeList = crep.GetUnitTypeList();
                    },
                    (object o) =>
                    {
                        if (string.IsNullOrWhiteSpace(UnitTypeInput))
                            return false;
                        return true;
                    });
                return _addUnitType;
            }
        }


        private ICommand _addUnitCommand;

        public ICommand AddUnitCommand
        {
            get
            {
                if (_addUnitCommand == null) _addUnitCommand = new RelayCommand(
                    (object o) =>
                    {
                        Validate validate = new Validate();
                        validate.AddValidator(new Validator<string>(UnitName, "Unit name",
                            new List<ISpecyficValidation<string>>()
                            {
                                new ValidateStringEmpty()
                            }));
                        validate.AddValidator(new Validator<string>(UnitSymbol, "Unit symbol",
                            new List<ISpecyficValidation<string>>()
                            {
                                new ValidateStringEmpty()
                            }));
                        validate.AddValidator(new Validator<string>(UnitRatio, "Unit ratio",
                            new List<ISpecyficValidation<string>>()
                            {
                                new ValidateStringEmpty(),
                                new ValidateStringIsDouble()
                            }));


                        if (!validate.Validation(out string message))
                        {
                            ErrorVisibility = Visible;
                            ErrorMessages = message;
                            return;
                        }

                        crep.AddUnit(UnitName, UnitSymbol, double.Parse(UnitRatio), SelectedUnitType);

                        //Clears Form
                        UnitName = string.Empty;
                        UnitSymbol = string.Empty;
                        UnitRatio = string.Empty;

                        //Updates DataGrid
                        UnitList = crep.GetUnitList(SelectedUnitType);
                        UnitsDataGridSource = crep.FillUnitsDataGrid(SelectedUnitType);

                        ErrorVisibility = Hidden;
                        ErrorMessages = string.Empty;
                    },
                    (object o) =>
                    {
                        if (SelectedUnitType == 0) return false;
                        return true;
                    });
                return _addUnitCommand;
            }
        }


        private ICommand _selectedUnitTypeCommand;

        public ICommand SelectedUnitTypeCommand
        {
            get
            {
                if (_selectedUnitTypeCommand == null) _selectedUnitTypeCommand = new RelayCommand(
                    (object o) =>
                    {
                        UnitsDataGridSource = crep.FillUnitsDataGrid(SelectedUnitType);
                    },
                    (object o) => true);
                return _selectedUnitTypeCommand;
            }
        }


        private ICommand _updateCommand;

        public ICommand UpdateCommand
        {
            get
            {
                if (_updateCommand == null) _updateCommand = new RelayCommand(
                    (object o) =>
                    {
                        crep.UpdateDatGrid();
                    },
                    (object o) => true);
                return _updateCommand;
            }
        }


        private ICommand _deleteCommand;

        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null) _deleteCommand = new RelayCommand(
                    (object o) =>
                    {
                        crep.DeleteRow(DataGridSelectedItems);

                        //Update DataGrids
                        UnitsDataGridSource = crep.FillUnitsDataGrid(SelectedUnitType);
                        UnitsTypeDataGridSource = crep.FillUnitsTypesDataGrid();

                        //Update UnitsType list
                        UnitTypeList = crep.GetUnitTypeList();
                    },
                    (object o) => true);
                return _deleteCommand;
            }
        }



        #endregion
    }
}

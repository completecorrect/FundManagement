using FundManagement.UI.Entity;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace FundManagement.UI.ViewModels
{
    public class LeftPaneViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        public LeftPaneViewModel()
        {
            _assetTypesList = new ObservableCollection<AssetType>();
            _assetTypesList.Add(new AssetType { Name = "Bond", Value = "Bond" });
            _assetTypesList.Add(new AssetType { Name = "Stock", Value = "Stock" });
        }

        private ObservableCollection<Asset> _assets = new ObservableCollection<Asset>();
        public ObservableCollection<Asset> Assets
        {
            get { return _assets; }
            set
            {
                _assets = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<AssetType> _assetTypesList;
        public ObservableCollection<AssetType> AssetTypesList
        {
            get { return _assetTypesList; }
            set
            {
                _assetTypesList = value;
                Validate(value);
                NotifyPropertyChanged();
            }
        }
        
        public class AssetType
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }



        private AssetType _selectedAssetType { get; set; }
        public AssetType SelectedAssetType
        {
            get { return _selectedAssetType; }
            set
            {
                _selectedAssetType = value;
                Validate(value);
                NotifyPropertyChanged();
            }
        }

        private string _price;
        public string Price
        {
            get { return _price; }
            set {
                _price = value;
                Validate(value);
                NotifyPropertyChanged();
            }
        }

        private string _quantity;
        public string Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                Validate(value);
                NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var safeDelegate = PropertyChanged;
            if (null != safeDelegate)
            {
                safeDelegate(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region INotifyDataErrorInfo

        private Dictionary<string, List<string>> errors = new Dictionary<string, List<string>>();

        public bool HasErrors
        {
            get
            {
                return errors.Any(err => err.Value != null && err.Value.Count > 0);
            }
        }
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public IEnumerable GetErrors([CallerMemberName]string propertyName="")
        {
            if(errors.ContainsKey(propertyName) && errors[propertyName].Count > 0)
            {
                return errors[propertyName].ToList();
            }
            return null;
        }
        
        /* Helper methods */
        public void NotifyErrorsChanged([CallerMemberName] string propertyName="")
        {
            var localDelegate = ErrorsChanged;
            if(null != localDelegate)
            {
                localDelegate(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }

        public void Validate(object valueObject, [CallerMemberName] string propertyName="")
        {
            //errors.Clear();

            string errorMessage=string.Empty;
            decimal tempDecimal;
            switch (propertyName)
            {
                case "Price":
                    if (!decimal.TryParse(Price, out tempDecimal))
                        errorMessage = "Price String is not valid decimal";
                    break;

                case "Quantity":
                    if (!decimal.TryParse(Quantity, out tempDecimal))
                        errorMessage = "Quantity String is not valid decimal";
                    break;
                case "SelectedAssetType":
                    if (null == valueObject)
                        errorMessage = "SelectedAssetType cannot be null";
                    break;
                default:
                    break;
            }


            if (!string.IsNullOrEmpty(errorMessage))
            {
                if (!errors.ContainsKey(propertyName))
                    errors.Add(propertyName, new List<string>());

                errors[propertyName].Add(errorMessage);
                NotifyErrorsChanged(propertyName);
            }
            else
            {   // clean the errors for that property
                if (errors.ContainsKey(propertyName))
                    errors[propertyName].Clear();
            }

        }

        #endregion
        
    }
}

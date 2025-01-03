using System;
using CapybaraAdventure.Ad;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using Zenject;

namespace CapybaraAdventure.Player
{
    [Serializable]
    public class ConsumablePurchase
    {
        public string Name;
        public string ID;
        public string Description;
        public float Price;
        public float Amount;
    }
    
    [Serializable]
    public class NonConsumablePurchase
    {
        public string Name;
        public string ID;
        public string Description;
        public float Price;
    }
    
    public class PurchaseManager : MonoBehaviour, IDetailedStoreListener
    {
        [SerializeField] private ConsumablePurchase _consumablePurchase;
        [SerializeField] private NonConsumablePurchase _nonConsumablePurchase;
        
        private IStoreController _storeController;
        private PlayerData _playerData;

        [Inject]
        private void Construct(PlayerData playerData)
        {
            _playerData = playerData;
        }

        private void Awake()
        {
            Setup();
        }

        // melons (consumable)
        public void InitiateConsumablePurchase()
        {
            _storeController.InitiatePurchase(_consumablePurchase.ID);
        }
        
        // NO ADS (non consumable)
        public void InitiateNonConsumablePurchase()
        {
            _storeController.InitiatePurchase(_nonConsumablePurchase.ID);
        }
        
        private void Setup()
        {
            ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            
            builder.AddProduct(_consumablePurchase.ID, ProductType.Consumable);
            builder.AddProduct(_nonConsumablePurchase.ID, ProductType.NonConsumable);
            
            UnityPurchasing.Initialize(this, builder);
            
            Debug.Log($"{nameof(PurchaseManager)}::Setup completed");
        }

        private void CheckNonConsumable()
        {
            var product = _storeController.products.WithID(_nonConsumablePurchase.ID);
            AdTimer.Instance.SetNoAds(product.hasReceipt);
        }

        private void ProcessConsumable()
        {
            _playerData.AddPurchasedMelons(Mathf.RoundToInt(_consumablePurchase.Amount));
        }
        
        private void ProcessNonConsumable()
        {
            AdTimer.Instance.SetNoAds(true);
        }
        
        #region StoreCallbacks

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            Debug.LogError($"{nameof(PurchaseManager)}::OnInitializeFailed: {error}");
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            Debug.LogError($"{nameof(PurchaseManager)}::OnInitializeFailed w/ message: {error}, {message}");
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            if (purchaseEvent.purchasedProduct.definition.id.Equals(_consumablePurchase.ID))
            {
                ProcessConsumable();
            }
            else if (purchaseEvent.purchasedProduct.definition.id.Equals(_nonConsumablePurchase.ID))
            {
                ProcessNonConsumable();
            }
            
            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            Debug.LogError($"{nameof(PurchaseManager)}::OnPurchaseFailed: {failureReason}");
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            _storeController = controller;
            
            CheckNonConsumable();
            Debug.Log($"{nameof(PurchaseManager)}::OnInitialized: successfully!");
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
        {
            Debug.LogError($"{nameof(PurchaseManager)}::OnPurchaseFailed: {failureDescription}");
        }

        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Firebase;
using Firebase.Analytics;
using Firebase.Crashlytics;
using Firebase.Extensions;
using UnityEngine;

namespace Core.Common.ThirdParty
{
    public enum EventName
    {
    }
    
    public static class FirebaseService
    {
        private const bool IsAnalyticsCollectionEnabled = true;
        private const bool IsCrashlyticsCollectionEnabled = true;
        private const bool IsReportUncaughtAsFatal = true;
        
        private static FirebaseApp _firebaseInstance = null;

        private static bool IsInitialized => _firebaseInstance != null;

        public static void Initialize()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
                return;
                
            try
            {
                _firebaseInstance = null;

                FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => 
                {
                    if (task.Result == DependencyStatus.Available) 
                    {
                        _firebaseInstance = FirebaseApp.DefaultInstance;

                        InitializeAnalytics();
                        InitializeCrashlytics();

                        Debug.Log($"Firebase services were successfully initialized!");
                    } 
                    else 
                    {
                        Debug.LogError($"{nameof(FirebaseService)}::{nameof(Initialize)}: Could not resolve all Firebase dependencies: {task.Result}");
    
                        if (task.Exception != null)
                            Debug.LogError($"{nameof(FirebaseService)}::{nameof(Initialize)}: Exception while resolution: {task.Exception.Message} \n {task.Exception.StackTrace}");
    
                        return;
                    }
                });
            }
            catch (Exception ex)
            {
                Debug.LogError($"{nameof(FirebaseService)}::{nameof(Initialize)}: {ex.Message} \n {ex.StackTrace}");
                _firebaseInstance = null;
            }
        }

        public static void LogEvent(EventName name) => 
            LogEvent(name.ToString(), null);

        public static void LogEvent(EventName name, params Parameter[] parameters) => 
            LogEvent(name.ToString(), parameters);

        private static void InitializeAnalytics()
        {
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(IsAnalyticsCollectionEnabled);
        }

        private static void InitializeCrashlytics()
        {
            Crashlytics.IsCrashlyticsCollectionEnabled = IsCrashlyticsCollectionEnabled;
            Crashlytics.ReportUncaughtExceptionsAsFatal = IsReportUncaughtAsFatal;
        }

        private static void LogEvent(string name, IEnumerable<Parameter> parameters)
        {
            try
            {
                if (IsInitialized == false)
                {
                    Debug.LogError($"{nameof(FirebaseService)}: Tried to log event before initialization!");
                    return;
                }
    
                if (parameters == null)
                {
                    FirebaseAnalytics.LogEvent(name);
                    Debug.Log($"Event '{name}' was successfully logged!");
                    return;
                }
    
                FirebaseAnalytics.LogEvent(name, parameters.ToArray());

                Debug.Log($"{nameof(FirebaseService)}: Event '{name}' was successfully logged!");
            }
            catch (Exception ex)
            {
                Debug.Log($"{nameof(FirebaseService)}::{nameof(LogEvent)}: {ex.Message} \n {ex.StackTrace}");
                _firebaseInstance = null;
            }
        }
    }
}
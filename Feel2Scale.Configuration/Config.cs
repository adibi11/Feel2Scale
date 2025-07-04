using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feel2Scale.Configuration
{
    public class Config
    {
        /// Singleton instance of IConfigurationRoot to hold the configuration settings
        private static IConfigurationRoot _configuration;
        /// Lock object to ensure thread-safe initialization of the configuration
        private static readonly object _lock = new object();

        /// <summary>
        /// Builds and initializes the configuration from appsettings.json
        static Config()
        {
            InitializeConfiguration();
        }
        /// <summary>
        /// Checks if the configuration is initialized and initializes it if not.
        private static void InitializeConfiguration()
        {
            if (_configuration == null)
            {
                // Replace this block inside InitializeConfiguration to ensure configDirectory is never null
                lock (_lock)
                {
                    if (_configuration == null)
                    {
                        // Walk up to the Configuration project directory if running from another project's output
                        var assemblyLocation = typeof(Config).Assembly.Location;
                        var configDirectory = Path.GetDirectoryName(assemblyLocation);

                        // Try to find the Configuration project directory by walking up from the output directory
                        // This assumes the output is in bin/{Debug|Release}/netX.Y/
                        string originalConfigDirectory = configDirectory;
                        for (int i = 0; i < 3; i++)
                        {
                            if (configDirectory == null) break;
                            var candidate = Path.Combine(configDirectory, "appsettings.json");
                            if (File.Exists(candidate))
                            {
                                
                                break;
                            }
                            configDirectory = Path.GetDirectoryName(configDirectory);
                        }

                        if (configDirectory == null)
                        {
                            throw new DirectoryNotFoundException($"Could not determine the Configuration project directory from assembly location: {assemblyLocation}");
                        }

                        var configPath = Path.Combine(configDirectory, "appsettings.json");

                        if (!File.Exists(configPath))
                        {
                            throw new FileNotFoundException($"Could not find appsettings.json in the Configuration project directory: {configPath}");
                        }

                        var builder = new ConfigurationBuilder()
                            .SetBasePath(configDirectory)
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

                        // Print the contents of appsettings.json
                        Console.WriteLine($"Loading configuration from: {configPath}");
                        var jsonContent = File.ReadAllText(configPath);
                        Console.WriteLine(jsonContent);

                        _configuration = builder.Build();

                        if (_configuration == null)
                        {
                            throw new InvalidOperationException("Configuration could not be loaded. Ensure appsettings.json exists and is valid.");
                        }
                    }
                }
            }
        }

        public static IConfigurationRoot GetConfiguration()
        {
            
            if (_configuration == null)
            {
                InitializeConfiguration();
            }
            return _configuration;
            
        }

        /// <summary>
        /// Gets the connection string by name from the configuration.
        public static string GetConnectionString(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Connection string name cannot be null or empty.", nameof(name));
            }
            
            var connectionString = _configuration.GetConnectionString(name);
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException($"Connection string '{name}' not found in configuration.");
            }

            return connectionString;
        }

        /// <summary>
        /// Getss the value of a configuration setting by its key.
        public static string GetSetting(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Configuration key cannot be null or empty.", nameof(key));
            }

            var section = _configuration.GetSection(key);
            if (!section.Exists())
            {
                throw new InvalidOperationException($"Configuration section '{key}' not found.");
            }

            var value = section.Get<string>();
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidOperationException($"Failed to deserialize the configuration section '{key}' into string.");
            }

            return value;
        }
    }
}

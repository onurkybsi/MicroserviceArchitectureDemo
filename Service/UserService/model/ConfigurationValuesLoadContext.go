package model

// ConfigurationValuesLoadContext context to load configuration values from .env and appsettings.json files
type ConfigurationValuesLoadContext struct {
	EnvFilePath         string
	AppSettingsFilePath string
}

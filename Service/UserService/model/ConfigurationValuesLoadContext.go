package model

// ConfigurationValuesLoadContext context to load configuration values from .env and appsettings.json files
type ConfigurationValuesLoadContext struct {
	Environment           Environment
	CofigurationFilesPath string
}

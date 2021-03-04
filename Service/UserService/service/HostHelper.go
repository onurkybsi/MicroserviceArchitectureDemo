package service

import (
	"fmt"
	"log"

	"userService/model"

	"github.com/joho/godotenv"
)

// LoadConfigurationValues load your values from .env and appsettings.json files
func LoadConfigurationValues(context model.ConfigurationValuesLoadContext) {
	loadEnvironmentVariablesValues(context)
	loadAppSettingsValues(context)
}

func loadEnvironmentVariablesValues(context model.ConfigurationValuesLoadContext) {
	envFilesPath := fmt.Sprintf(`%v\%v.env`, context.CofigurationFilesPath, context.Environment)
	err := godotenv.Load(envFilesPath)
	if err != nil {
		log.Fatalf("Error when loading .env file")
	}
}

func loadAppSettingsValues(context model.ConfigurationValuesLoadContext) {}

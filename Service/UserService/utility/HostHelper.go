package utility

import (
	"encoding/json"
	"fmt"
	"io/ioutil"
	"log"
	"os"

	"userService/model"

	"github.com/joho/godotenv"
)

var appSettingsValues map[string]string

// LoadConfigurationValues load your values from .env and appsettings.json files
func LoadConfigurationValues(context model.ConfigurationValuesLoadContext) func(key string) string {
	loadEnvironmentVariablesValues(context)
	loadAppSettingsValues(context)

	return func(key string) string {
		var value string

		envValue, envValueExistsForTheKey := os.LookupEnv(key)

		if envValueExistsForTheKey {
			value = envValue
		} else {
			value = appSettingsValues[key]
		}
		return value
	}
}

func loadEnvironmentVariablesValues(context model.ConfigurationValuesLoadContext) {
	if len(context.EnvFilePath) <= 0 {
		return
	}

	err := godotenv.Load(context.EnvFilePath)
	logLoadingConfigurationValuesError(err, "appsettings")
}

func loadAppSettingsValues(context model.ConfigurationValuesLoadContext) {
	if len(context.AppSettingsFilePath) <= 0 {
		return
	}

	data, err := ioutil.ReadFile(context.AppSettingsFilePath)
	logLoadingConfigurationValuesError(err, "appsettings")

	err = json.Unmarshal(data, &appSettingsValues)
	logLoadingConfigurationValuesError(err, "appsettings")
}

func logLoadingConfigurationValuesError(err error, configurationFileName string) {
	if err != nil {
		errorMessage := fmt.Sprintf(`Error occurred when loading %v file`, configurationFileName)
		log.Fatalf(errorMessage)
	}
}

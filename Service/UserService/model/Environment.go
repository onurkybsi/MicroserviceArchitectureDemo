package model

// Environment specifies environment name
type Environment string

const (
	// DEV envrionment specifier
	DEV Environment = "DEV"
	// STAGING envrionment specifier
	STAGING Environment = "STAGING"
	// PROD envrionment specifier
	PROD Environment = "PROD"
)

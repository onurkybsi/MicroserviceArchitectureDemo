package main

import (
	"context"
	"log"
	service "userService/grpc-base"
)

// UserService user service grpc implementation
type UserService struct{}

// VerifyUser Method of UserService that verify if the user is authenticated
func (service *UserService) VerifyUser(ctx context.Context, verifyUserRequest *service.VerifyUserRequest) (*service.VerifyUserResponse, error) {
	log.Fatalln("VerifyUser called !")
	return nil, nil
}

// AuthenticateUser Method of UserService that authenticate user
func (service *UserService) AuthenticateUser(ctx context.Context, authenticateUserRequest *service.AuthenticateUserRequest) (*service.AuthenticateUserResponse, error) {
	log.Fatalln("AuthenticateUser called !")
	return nil, nil
}

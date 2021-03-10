package main

import (
	"fmt"
	"log"
	"net"
	"userService/model"
	"userService/service"

	"google.golang.org/grpc"
)

func main() {
	fmt.Println("Hello from UserService !")

	envFilePath := fmt.Sprintf(`.\%v.env`, model.DEV)
	appsettingsFilePath := fmt.Sprintf(`.\%v`, "appsettings.json")
	confValueGetter := service.LoadConfigurationValues(model.ConfigurationValuesLoadContext{EnvFilePath: envFilePath, AppSettingsFilePath: appsettingsFilePath})

	tcpPort := confValueGetter("SERVER_PORT")

	lis, err := net.Listen("tcp", fmt.Sprintf(":%v", tcpPort))
	if err != nil {
		log.Fatalf("Failed to listen on port 9000: %v", err)
	} else {
		log.Printf("Listen on port %v\n", tcpPort)
	}

	grpcServer := grpc.NewServer()
	if err := grpcServer.Serve(lis); err != nil {
		log.Fatalf("Failed to serve gRPC server over port 9000: %v", err)
	} else {
		log.Printf("Serve gRPC server over port %v\n", tcpPort)
	}
}

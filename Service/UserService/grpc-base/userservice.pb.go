// Code generated by protoc-gen-go. DO NOT EDIT.
// versions:
// 	protoc-gen-go v1.25.0
// 	protoc        v3.15.3
// source: userservice.proto

package service

import (
	proto "github.com/golang/protobuf/proto"
	protoreflect "google.golang.org/protobuf/reflect/protoreflect"
	protoimpl "google.golang.org/protobuf/runtime/protoimpl"
	reflect "reflect"
	sync "sync"
)

const (
	// Verify that this generated code is sufficiently up-to-date.
	_ = protoimpl.EnforceVersion(20 - protoimpl.MinVersion)
	// Verify that runtime/protoimpl is sufficiently up-to-date.
	_ = protoimpl.EnforceVersion(protoimpl.MaxVersion - 20)
)

// This is a compile-time assertion that a sufficiently up-to-date version
// of the legacy proto package is being used.
const _ = proto.ProtoPackageIsVersion4

type VerifyUserRequest struct {
	state         protoimpl.MessageState
	sizeCache     protoimpl.SizeCache
	unknownFields protoimpl.UnknownFields

	Token string `protobuf:"bytes,1,opt,name=token,proto3" json:"token,omitempty"`
}

func (x *VerifyUserRequest) Reset() {
	*x = VerifyUserRequest{}
	if protoimpl.UnsafeEnabled {
		mi := &file_userservice_proto_msgTypes[0]
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		ms.StoreMessageInfo(mi)
	}
}

func (x *VerifyUserRequest) String() string {
	return protoimpl.X.MessageStringOf(x)
}

func (*VerifyUserRequest) ProtoMessage() {}

func (x *VerifyUserRequest) ProtoReflect() protoreflect.Message {
	mi := &file_userservice_proto_msgTypes[0]
	if protoimpl.UnsafeEnabled && x != nil {
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		if ms.LoadMessageInfo() == nil {
			ms.StoreMessageInfo(mi)
		}
		return ms
	}
	return mi.MessageOf(x)
}

// Deprecated: Use VerifyUserRequest.ProtoReflect.Descriptor instead.
func (*VerifyUserRequest) Descriptor() ([]byte, []int) {
	return file_userservice_proto_rawDescGZIP(), []int{0}
}

func (x *VerifyUserRequest) GetToken() string {
	if x != nil {
		return x.Token
	}
	return ""
}

type VerifyUserResponse struct {
	state         protoimpl.MessageState
	sizeCache     protoimpl.SizeCache
	unknownFields protoimpl.UnknownFields

	ServiceProcessResult *ServiceProcessResult `protobuf:"bytes,1,opt,name=serviceProcessResult,proto3" json:"serviceProcessResult,omitempty"`
	AuthenticationResult *AuthenticationResult `protobuf:"bytes,2,opt,name=authenticationResult,proto3" json:"authenticationResult,omitempty"`
}

func (x *VerifyUserResponse) Reset() {
	*x = VerifyUserResponse{}
	if protoimpl.UnsafeEnabled {
		mi := &file_userservice_proto_msgTypes[1]
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		ms.StoreMessageInfo(mi)
	}
}

func (x *VerifyUserResponse) String() string {
	return protoimpl.X.MessageStringOf(x)
}

func (*VerifyUserResponse) ProtoMessage() {}

func (x *VerifyUserResponse) ProtoReflect() protoreflect.Message {
	mi := &file_userservice_proto_msgTypes[1]
	if protoimpl.UnsafeEnabled && x != nil {
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		if ms.LoadMessageInfo() == nil {
			ms.StoreMessageInfo(mi)
		}
		return ms
	}
	return mi.MessageOf(x)
}

// Deprecated: Use VerifyUserResponse.ProtoReflect.Descriptor instead.
func (*VerifyUserResponse) Descriptor() ([]byte, []int) {
	return file_userservice_proto_rawDescGZIP(), []int{1}
}

func (x *VerifyUserResponse) GetServiceProcessResult() *ServiceProcessResult {
	if x != nil {
		return x.ServiceProcessResult
	}
	return nil
}

func (x *VerifyUserResponse) GetAuthenticationResult() *AuthenticationResult {
	if x != nil {
		return x.AuthenticationResult
	}
	return nil
}

type AuthenticateUserRequest struct {
	state         protoimpl.MessageState
	sizeCache     protoimpl.SizeCache
	unknownFields protoimpl.UnknownFields

	Email    string `protobuf:"bytes,1,opt,name=email,proto3" json:"email,omitempty"`
	Password string `protobuf:"bytes,2,opt,name=password,proto3" json:"password,omitempty"`
}

func (x *AuthenticateUserRequest) Reset() {
	*x = AuthenticateUserRequest{}
	if protoimpl.UnsafeEnabled {
		mi := &file_userservice_proto_msgTypes[2]
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		ms.StoreMessageInfo(mi)
	}
}

func (x *AuthenticateUserRequest) String() string {
	return protoimpl.X.MessageStringOf(x)
}

func (*AuthenticateUserRequest) ProtoMessage() {}

func (x *AuthenticateUserRequest) ProtoReflect() protoreflect.Message {
	mi := &file_userservice_proto_msgTypes[2]
	if protoimpl.UnsafeEnabled && x != nil {
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		if ms.LoadMessageInfo() == nil {
			ms.StoreMessageInfo(mi)
		}
		return ms
	}
	return mi.MessageOf(x)
}

// Deprecated: Use AuthenticateUserRequest.ProtoReflect.Descriptor instead.
func (*AuthenticateUserRequest) Descriptor() ([]byte, []int) {
	return file_userservice_proto_rawDescGZIP(), []int{2}
}

func (x *AuthenticateUserRequest) GetEmail() string {
	if x != nil {
		return x.Email
	}
	return ""
}

func (x *AuthenticateUserRequest) GetPassword() string {
	if x != nil {
		return x.Password
	}
	return ""
}

type AuthenticateUserResponse struct {
	state         protoimpl.MessageState
	sizeCache     protoimpl.SizeCache
	unknownFields protoimpl.UnknownFields

	ServiceProcessResult *ServiceProcessResult `protobuf:"bytes,1,opt,name=serviceProcessResult,proto3" json:"serviceProcessResult,omitempty"`
	AuthenticationResult *AuthenticationResult `protobuf:"bytes,2,opt,name=authenticationResult,proto3" json:"authenticationResult,omitempty"`
}

func (x *AuthenticateUserResponse) Reset() {
	*x = AuthenticateUserResponse{}
	if protoimpl.UnsafeEnabled {
		mi := &file_userservice_proto_msgTypes[3]
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		ms.StoreMessageInfo(mi)
	}
}

func (x *AuthenticateUserResponse) String() string {
	return protoimpl.X.MessageStringOf(x)
}

func (*AuthenticateUserResponse) ProtoMessage() {}

func (x *AuthenticateUserResponse) ProtoReflect() protoreflect.Message {
	mi := &file_userservice_proto_msgTypes[3]
	if protoimpl.UnsafeEnabled && x != nil {
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		if ms.LoadMessageInfo() == nil {
			ms.StoreMessageInfo(mi)
		}
		return ms
	}
	return mi.MessageOf(x)
}

// Deprecated: Use AuthenticateUserResponse.ProtoReflect.Descriptor instead.
func (*AuthenticateUserResponse) Descriptor() ([]byte, []int) {
	return file_userservice_proto_rawDescGZIP(), []int{3}
}

func (x *AuthenticateUserResponse) GetServiceProcessResult() *ServiceProcessResult {
	if x != nil {
		return x.ServiceProcessResult
	}
	return nil
}

func (x *AuthenticateUserResponse) GetAuthenticationResult() *AuthenticationResult {
	if x != nil {
		return x.AuthenticationResult
	}
	return nil
}

type AuthenticationResult struct {
	state         protoimpl.MessageState
	sizeCache     protoimpl.SizeCache
	unknownFields protoimpl.UnknownFields

	IsAuthenticated bool   `protobuf:"varint,1,opt,name=isAuthenticated,proto3" json:"isAuthenticated,omitempty"`
	Message         string `protobuf:"bytes,2,opt,name=message,proto3" json:"message,omitempty"`
}

func (x *AuthenticationResult) Reset() {
	*x = AuthenticationResult{}
	if protoimpl.UnsafeEnabled {
		mi := &file_userservice_proto_msgTypes[4]
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		ms.StoreMessageInfo(mi)
	}
}

func (x *AuthenticationResult) String() string {
	return protoimpl.X.MessageStringOf(x)
}

func (*AuthenticationResult) ProtoMessage() {}

func (x *AuthenticationResult) ProtoReflect() protoreflect.Message {
	mi := &file_userservice_proto_msgTypes[4]
	if protoimpl.UnsafeEnabled && x != nil {
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		if ms.LoadMessageInfo() == nil {
			ms.StoreMessageInfo(mi)
		}
		return ms
	}
	return mi.MessageOf(x)
}

// Deprecated: Use AuthenticationResult.ProtoReflect.Descriptor instead.
func (*AuthenticationResult) Descriptor() ([]byte, []int) {
	return file_userservice_proto_rawDescGZIP(), []int{4}
}

func (x *AuthenticationResult) GetIsAuthenticated() bool {
	if x != nil {
		return x.IsAuthenticated
	}
	return false
}

func (x *AuthenticationResult) GetMessage() string {
	if x != nil {
		return x.Message
	}
	return ""
}

var File_userservice_proto protoreflect.FileDescriptor

var file_userservice_proto_rawDesc = []byte{
	0x0a, 0x11, 0x75, 0x73, 0x65, 0x72, 0x73, 0x65, 0x72, 0x76, 0x69, 0x63, 0x65, 0x2e, 0x70, 0x72,
	0x6f, 0x74, 0x6f, 0x12, 0x07, 0x73, 0x65, 0x72, 0x76, 0x69, 0x63, 0x65, 0x1a, 0x0a, 0x62, 0x61,
	0x73, 0x65, 0x2e, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x22, 0x29, 0x0a, 0x11, 0x56, 0x65, 0x72, 0x69,
	0x66, 0x79, 0x55, 0x73, 0x65, 0x72, 0x52, 0x65, 0x71, 0x75, 0x65, 0x73, 0x74, 0x12, 0x14, 0x0a,
	0x05, 0x74, 0x6f, 0x6b, 0x65, 0x6e, 0x18, 0x01, 0x20, 0x01, 0x28, 0x09, 0x52, 0x05, 0x74, 0x6f,
	0x6b, 0x65, 0x6e, 0x22, 0xba, 0x01, 0x0a, 0x12, 0x56, 0x65, 0x72, 0x69, 0x66, 0x79, 0x55, 0x73,
	0x65, 0x72, 0x52, 0x65, 0x73, 0x70, 0x6f, 0x6e, 0x73, 0x65, 0x12, 0x51, 0x0a, 0x14, 0x73, 0x65,
	0x72, 0x76, 0x69, 0x63, 0x65, 0x50, 0x72, 0x6f, 0x63, 0x65, 0x73, 0x73, 0x52, 0x65, 0x73, 0x75,
	0x6c, 0x74, 0x18, 0x01, 0x20, 0x01, 0x28, 0x0b, 0x32, 0x1d, 0x2e, 0x73, 0x65, 0x72, 0x76, 0x69,
	0x63, 0x65, 0x2e, 0x53, 0x65, 0x72, 0x76, 0x69, 0x63, 0x65, 0x50, 0x72, 0x6f, 0x63, 0x65, 0x73,
	0x73, 0x52, 0x65, 0x73, 0x75, 0x6c, 0x74, 0x52, 0x14, 0x73, 0x65, 0x72, 0x76, 0x69, 0x63, 0x65,
	0x50, 0x72, 0x6f, 0x63, 0x65, 0x73, 0x73, 0x52, 0x65, 0x73, 0x75, 0x6c, 0x74, 0x12, 0x51, 0x0a,
	0x14, 0x61, 0x75, 0x74, 0x68, 0x65, 0x6e, 0x74, 0x69, 0x63, 0x61, 0x74, 0x69, 0x6f, 0x6e, 0x52,
	0x65, 0x73, 0x75, 0x6c, 0x74, 0x18, 0x02, 0x20, 0x01, 0x28, 0x0b, 0x32, 0x1d, 0x2e, 0x73, 0x65,
	0x72, 0x76, 0x69, 0x63, 0x65, 0x2e, 0x41, 0x75, 0x74, 0x68, 0x65, 0x6e, 0x74, 0x69, 0x63, 0x61,
	0x74, 0x69, 0x6f, 0x6e, 0x52, 0x65, 0x73, 0x75, 0x6c, 0x74, 0x52, 0x14, 0x61, 0x75, 0x74, 0x68,
	0x65, 0x6e, 0x74, 0x69, 0x63, 0x61, 0x74, 0x69, 0x6f, 0x6e, 0x52, 0x65, 0x73, 0x75, 0x6c, 0x74,
	0x22, 0x4b, 0x0a, 0x17, 0x41, 0x75, 0x74, 0x68, 0x65, 0x6e, 0x74, 0x69, 0x63, 0x61, 0x74, 0x65,
	0x55, 0x73, 0x65, 0x72, 0x52, 0x65, 0x71, 0x75, 0x65, 0x73, 0x74, 0x12, 0x14, 0x0a, 0x05, 0x65,
	0x6d, 0x61, 0x69, 0x6c, 0x18, 0x01, 0x20, 0x01, 0x28, 0x09, 0x52, 0x05, 0x65, 0x6d, 0x61, 0x69,
	0x6c, 0x12, 0x1a, 0x0a, 0x08, 0x70, 0x61, 0x73, 0x73, 0x77, 0x6f, 0x72, 0x64, 0x18, 0x02, 0x20,
	0x01, 0x28, 0x09, 0x52, 0x08, 0x70, 0x61, 0x73, 0x73, 0x77, 0x6f, 0x72, 0x64, 0x22, 0xc0, 0x01,
	0x0a, 0x18, 0x41, 0x75, 0x74, 0x68, 0x65, 0x6e, 0x74, 0x69, 0x63, 0x61, 0x74, 0x65, 0x55, 0x73,
	0x65, 0x72, 0x52, 0x65, 0x73, 0x70, 0x6f, 0x6e, 0x73, 0x65, 0x12, 0x51, 0x0a, 0x14, 0x73, 0x65,
	0x72, 0x76, 0x69, 0x63, 0x65, 0x50, 0x72, 0x6f, 0x63, 0x65, 0x73, 0x73, 0x52, 0x65, 0x73, 0x75,
	0x6c, 0x74, 0x18, 0x01, 0x20, 0x01, 0x28, 0x0b, 0x32, 0x1d, 0x2e, 0x73, 0x65, 0x72, 0x76, 0x69,
	0x63, 0x65, 0x2e, 0x53, 0x65, 0x72, 0x76, 0x69, 0x63, 0x65, 0x50, 0x72, 0x6f, 0x63, 0x65, 0x73,
	0x73, 0x52, 0x65, 0x73, 0x75, 0x6c, 0x74, 0x52, 0x14, 0x73, 0x65, 0x72, 0x76, 0x69, 0x63, 0x65,
	0x50, 0x72, 0x6f, 0x63, 0x65, 0x73, 0x73, 0x52, 0x65, 0x73, 0x75, 0x6c, 0x74, 0x12, 0x51, 0x0a,
	0x14, 0x61, 0x75, 0x74, 0x68, 0x65, 0x6e, 0x74, 0x69, 0x63, 0x61, 0x74, 0x69, 0x6f, 0x6e, 0x52,
	0x65, 0x73, 0x75, 0x6c, 0x74, 0x18, 0x02, 0x20, 0x01, 0x28, 0x0b, 0x32, 0x1d, 0x2e, 0x73, 0x65,
	0x72, 0x76, 0x69, 0x63, 0x65, 0x2e, 0x41, 0x75, 0x74, 0x68, 0x65, 0x6e, 0x74, 0x69, 0x63, 0x61,
	0x74, 0x69, 0x6f, 0x6e, 0x52, 0x65, 0x73, 0x75, 0x6c, 0x74, 0x52, 0x14, 0x61, 0x75, 0x74, 0x68,
	0x65, 0x6e, 0x74, 0x69, 0x63, 0x61, 0x74, 0x69, 0x6f, 0x6e, 0x52, 0x65, 0x73, 0x75, 0x6c, 0x74,
	0x22, 0x5a, 0x0a, 0x14, 0x41, 0x75, 0x74, 0x68, 0x65, 0x6e, 0x74, 0x69, 0x63, 0x61, 0x74, 0x69,
	0x6f, 0x6e, 0x52, 0x65, 0x73, 0x75, 0x6c, 0x74, 0x12, 0x28, 0x0a, 0x0f, 0x69, 0x73, 0x41, 0x75,
	0x74, 0x68, 0x65, 0x6e, 0x74, 0x69, 0x63, 0x61, 0x74, 0x65, 0x64, 0x18, 0x01, 0x20, 0x01, 0x28,
	0x08, 0x52, 0x0f, 0x69, 0x73, 0x41, 0x75, 0x74, 0x68, 0x65, 0x6e, 0x74, 0x69, 0x63, 0x61, 0x74,
	0x65, 0x64, 0x12, 0x18, 0x0a, 0x07, 0x6d, 0x65, 0x73, 0x73, 0x61, 0x67, 0x65, 0x18, 0x02, 0x20,
	0x01, 0x28, 0x09, 0x52, 0x07, 0x6d, 0x65, 0x73, 0x73, 0x61, 0x67, 0x65, 0x32, 0xad, 0x01, 0x0a,
	0x0b, 0x55, 0x73, 0x65, 0x72, 0x53, 0x65, 0x72, 0x76, 0x69, 0x63, 0x65, 0x12, 0x45, 0x0a, 0x0a,
	0x56, 0x65, 0x72, 0x69, 0x66, 0x79, 0x55, 0x73, 0x65, 0x72, 0x12, 0x1a, 0x2e, 0x73, 0x65, 0x72,
	0x76, 0x69, 0x63, 0x65, 0x2e, 0x56, 0x65, 0x72, 0x69, 0x66, 0x79, 0x55, 0x73, 0x65, 0x72, 0x52,
	0x65, 0x71, 0x75, 0x65, 0x73, 0x74, 0x1a, 0x1b, 0x2e, 0x73, 0x65, 0x72, 0x76, 0x69, 0x63, 0x65,
	0x2e, 0x56, 0x65, 0x72, 0x69, 0x66, 0x79, 0x55, 0x73, 0x65, 0x72, 0x52, 0x65, 0x73, 0x70, 0x6f,
	0x6e, 0x73, 0x65, 0x12, 0x57, 0x0a, 0x10, 0x41, 0x75, 0x74, 0x68, 0x65, 0x6e, 0x74, 0x69, 0x63,
	0x61, 0x74, 0x65, 0x55, 0x73, 0x65, 0x72, 0x12, 0x20, 0x2e, 0x73, 0x65, 0x72, 0x76, 0x69, 0x63,
	0x65, 0x2e, 0x41, 0x75, 0x74, 0x68, 0x65, 0x6e, 0x74, 0x69, 0x63, 0x61, 0x74, 0x65, 0x55, 0x73,
	0x65, 0x72, 0x52, 0x65, 0x71, 0x75, 0x65, 0x73, 0x74, 0x1a, 0x21, 0x2e, 0x73, 0x65, 0x72, 0x76,
	0x69, 0x63, 0x65, 0x2e, 0x41, 0x75, 0x74, 0x68, 0x65, 0x6e, 0x74, 0x69, 0x63, 0x61, 0x74, 0x65,
	0x55, 0x73, 0x65, 0x72, 0x52, 0x65, 0x73, 0x70, 0x6f, 0x6e, 0x73, 0x65, 0x62, 0x06, 0x70, 0x72,
	0x6f, 0x74, 0x6f, 0x33,
}

var (
	file_userservice_proto_rawDescOnce sync.Once
	file_userservice_proto_rawDescData = file_userservice_proto_rawDesc
)

func file_userservice_proto_rawDescGZIP() []byte {
	file_userservice_proto_rawDescOnce.Do(func() {
		file_userservice_proto_rawDescData = protoimpl.X.CompressGZIP(file_userservice_proto_rawDescData)
	})
	return file_userservice_proto_rawDescData
}

var file_userservice_proto_msgTypes = make([]protoimpl.MessageInfo, 5)
var file_userservice_proto_goTypes = []interface{}{
	(*VerifyUserRequest)(nil),        // 0: service.VerifyUserRequest
	(*VerifyUserResponse)(nil),       // 1: service.VerifyUserResponse
	(*AuthenticateUserRequest)(nil),  // 2: service.AuthenticateUserRequest
	(*AuthenticateUserResponse)(nil), // 3: service.AuthenticateUserResponse
	(*AuthenticationResult)(nil),     // 4: service.AuthenticationResult
	(*ServiceProcessResult)(nil),     // 5: service.ServiceProcessResult
}
var file_userservice_proto_depIdxs = []int32{
	5, // 0: service.VerifyUserResponse.serviceProcessResult:type_name -> service.ServiceProcessResult
	4, // 1: service.VerifyUserResponse.authenticationResult:type_name -> service.AuthenticationResult
	5, // 2: service.AuthenticateUserResponse.serviceProcessResult:type_name -> service.ServiceProcessResult
	4, // 3: service.AuthenticateUserResponse.authenticationResult:type_name -> service.AuthenticationResult
	0, // 4: service.UserService.VerifyUser:input_type -> service.VerifyUserRequest
	2, // 5: service.UserService.AuthenticateUser:input_type -> service.AuthenticateUserRequest
	1, // 6: service.UserService.VerifyUser:output_type -> service.VerifyUserResponse
	3, // 7: service.UserService.AuthenticateUser:output_type -> service.AuthenticateUserResponse
	6, // [6:8] is the sub-list for method output_type
	4, // [4:6] is the sub-list for method input_type
	4, // [4:4] is the sub-list for extension type_name
	4, // [4:4] is the sub-list for extension extendee
	0, // [0:4] is the sub-list for field type_name
}

func init() { file_userservice_proto_init() }
func file_userservice_proto_init() {
	if File_userservice_proto != nil {
		return
	}
	file_base_proto_init()
	if !protoimpl.UnsafeEnabled {
		file_userservice_proto_msgTypes[0].Exporter = func(v interface{}, i int) interface{} {
			switch v := v.(*VerifyUserRequest); i {
			case 0:
				return &v.state
			case 1:
				return &v.sizeCache
			case 2:
				return &v.unknownFields
			default:
				return nil
			}
		}
		file_userservice_proto_msgTypes[1].Exporter = func(v interface{}, i int) interface{} {
			switch v := v.(*VerifyUserResponse); i {
			case 0:
				return &v.state
			case 1:
				return &v.sizeCache
			case 2:
				return &v.unknownFields
			default:
				return nil
			}
		}
		file_userservice_proto_msgTypes[2].Exporter = func(v interface{}, i int) interface{} {
			switch v := v.(*AuthenticateUserRequest); i {
			case 0:
				return &v.state
			case 1:
				return &v.sizeCache
			case 2:
				return &v.unknownFields
			default:
				return nil
			}
		}
		file_userservice_proto_msgTypes[3].Exporter = func(v interface{}, i int) interface{} {
			switch v := v.(*AuthenticateUserResponse); i {
			case 0:
				return &v.state
			case 1:
				return &v.sizeCache
			case 2:
				return &v.unknownFields
			default:
				return nil
			}
		}
		file_userservice_proto_msgTypes[4].Exporter = func(v interface{}, i int) interface{} {
			switch v := v.(*AuthenticationResult); i {
			case 0:
				return &v.state
			case 1:
				return &v.sizeCache
			case 2:
				return &v.unknownFields
			default:
				return nil
			}
		}
	}
	type x struct{}
	out := protoimpl.TypeBuilder{
		File: protoimpl.DescBuilder{
			GoPackagePath: reflect.TypeOf(x{}).PkgPath(),
			RawDescriptor: file_userservice_proto_rawDesc,
			NumEnums:      0,
			NumMessages:   5,
			NumExtensions: 0,
			NumServices:   1,
		},
		GoTypes:           file_userservice_proto_goTypes,
		DependencyIndexes: file_userservice_proto_depIdxs,
		MessageInfos:      file_userservice_proto_msgTypes,
	}.Build()
	File_userservice_proto = out.File
	file_userservice_proto_rawDesc = nil
	file_userservice_proto_goTypes = nil
	file_userservice_proto_depIdxs = nil
}
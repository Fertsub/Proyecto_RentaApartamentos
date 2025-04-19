// src/screens/HomeScreen.tsx
import React from "react";
import { View, Text, Pressable, ScrollView } from "react-native";
import { useNavigation } from "@react-navigation/native";
import { NativeStackNavigationProp } from "@react-navigation/native-stack";
import { FileText, User, Wrench } from "lucide-react-native";
import type { RootStackParamList } from "../navigation/AppNav";

export default function HomeScreen() {
  const navigation =
    useNavigation<NativeStackNavigationProp<RootStackParamList>>();

  return (
    <View className="flex-1 bg-gray-50">
      {/* Header */}
      <View className="bg-white border-b border-gray-200 px-4 py-3">
        <View className="flex-row justify-between items-center">
          <Text className="text-xl font-bold text-gray-900">
            Portal del Inquilino
          </Text>
          <Pressable
            onPress={() => navigation.navigate("Profile")}
            className="p-2"
            accessibilityLabel="Ir a perfil"
          >
            <User className="w-6 h-6 text-gray-700" />
          </Pressable>
        </View>
      </View>

      {/* Main Content */}
      <ScrollView contentContainerStyle={{ padding: 16 }}>
        <View className="gap-4">
          {/* Card: Quejas */}
          <Pressable
            onPress={() => navigation.navigate("Complaints")}
            className="flex-row items-center p-4 bg-white rounded-2xl shadow-sm active:shadow-md"
          >
            <View className="w-12 h-12 rounded-full bg-red-100 items-center justify-center mr-4">
              <FileText className="w-6 h-6 text-red-600" />
            </View>
            <View className="flex-1">
              <Text className="text-lg font-semibold text-gray-900">
                Quejas
              </Text>
              <Text className="text-sm text-gray-500">
                Enviar una queja al administrador
              </Text>
            </View>
          </Pressable>

          {/* Card: Mantenimiento */}
          <Pressable
            onPress={() => console.log("Mantenimiento")}
            className="flex-row items-center p-4 bg-white rounded-2xl shadow-sm active:shadow-md"
          >
            <View className="w-12 h-12 rounded-full bg-blue-100 items-center justify-center mr-4">
              <Wrench className="w-6 h-6 text-blue-600" />
            </View>
            <View className="flex-1">
              <Text className="text-lg font-semibold text-gray-900">
                Mantenimiento
              </Text>
              <Text className="text-sm text-gray-500">
                Solicitar servicio de mantenimiento
              </Text>
            </View>
          </Pressable>

          {/* Card: Perfil */}
          <Pressable
            onPress={() => navigation.navigate("Profile")}
            className="flex-row items-center p-4 bg-white rounded-2xl shadow-sm active:shadow-md"
          >
            <View className="w-12 h-12 rounded-full bg-green-100 items-center justify-center mr-4">
              <User className="w-6 h-6 text-green-600" />
            </View>
            <View className="flex-1">
              <Text className="text-lg font-semibold text-gray-900">
                Mi Información
              </Text>
              <Text className="text-sm text-gray-500">
                Ver y editar información personal
              </Text>
            </View>
          </Pressable>
        </View>
      </ScrollView>

      {/* Footer */}
      <View className="bg-white border-t border-gray-200 py-4">
        <Text className="text-center text-sm text-gray-500">
          © 2025 Portal del Inquilino
        </Text>
      </View>
    </View>
  );
}

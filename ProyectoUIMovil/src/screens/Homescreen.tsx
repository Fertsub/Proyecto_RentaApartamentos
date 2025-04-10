// src/screens/HomeScreen.tsx
import React from 'react';
import { View, Text, Pressable, ScrollView } from 'react-native';
import { Link } from 'expo-router'; // Cambiamos next/link por expo-router
import { FileText, User, Wrench } from 'lucide-react-native';

export default function HomeScreen() {
  return (
    <View className="flex-1 bg-gray-50">
      {/* Header */}
      <View className="sticky top-0 z-10 bg-white border-b border-gray-200">
        <View className="container flex flex-row items-center justify-between h-16 px-4 mx-auto">
          <Text className="text-xl font-bold">Portal del Inquilino</Text>
          <Link href="/profile" asChild>
            <Pressable className="p-2">
              <User className="w-6 h-6" />
              <Text className="sr-only">Perfil</Text>
            </Pressable>
          </Link>
        </View>
      </View>

      {/* Main Content */}
      <ScrollView className="flex-1 px-4 py-8">
        <View className="gap-4 md:gap-6 md:flex-row md:flex-wrap">
          {/* Quejas Card */}
          <Link href="/complaints" asChild>
            <Pressable className="flex-row items-center p-4 bg-white rounded-lg shadow-sm shadow-black/5 mb-4 active:shadow-md">
              <View className="items-center justify-center w-12 h-12 bg-red-100 rounded-full mr-4">
                <FileText className="w-6 h-6 text-red-600" />
              </View>
              <View className="flex-1">
                <Text className="text-lg font-medium">Quejas</Text>
                <Text className="text-sm text-gray-500">
                  Enviar una queja al administrador
                </Text>
              </View>
            </Pressable>
          </Link>

          {/* Mantenimiento Card */}
          <Link href="/maintenance" asChild>
            <Pressable className="flex-row items-center p-4 bg-white rounded-lg shadow-sm shadow-black/5 mb-4 active:shadow-md">
              <View className="items-center justify-center w-12 h-12 bg-blue-100 rounded-full mr-4">
                <Wrench className="w-6 h-6 text-blue-600" />
              </View>
              <View className="flex-1">
                <Text className="text-lg font-medium">Mantenimiento</Text>
                <Text className="text-sm text-gray-500">
                  Solicitar servicio de mantenimiento
                </Text>
              </View>
            </Pressable>
          </Link>

          {/* Perfil Card */}
          <Link href="/profile" asChild>
            <Pressable className="flex-row items-center p-4 bg-white rounded-lg shadow-sm shadow-black/5 mb-4 active:shadow-md">
              <View className="items-center justify-center w-12 h-12 bg-green-100 rounded-full mr-4">
                <User className="w-6 h-6 text-green-600" />
              </View>
              <View className="flex-1">
                <Text className="text-lg font-medium">Mi Información</Text>
                <Text className="text-sm text-gray-500">
                  Ver y editar información personal
                </Text>
              </View>
            </Pressable>
          </Link>
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
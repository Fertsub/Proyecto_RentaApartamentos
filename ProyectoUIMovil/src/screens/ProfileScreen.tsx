// src/screens/ProfileScreen.tsx
import React from "react";
import { View, Text, Pressable } from "react-native";
import { NativeStackScreenProps } from "@react-navigation/native-stack";
import { RootStackParamList } from "../navigation/AppNav";

type Props = NativeStackScreenProps<RootStackParamList, "Profile">;

export default function ProfileScreen({ route, navigation }: Props) {
  const { userId } = route.params || {};

  return (
    <View className="flex-1 bg-white px-6 py-8">
      <Text className="text-2xl font-bold text-gray-900 mb-4">Mi Perfil</Text>
      <Text className="text-base text-gray-700 mb-6">
        ID de Usuario:{" "}
        <Text className="font-semibold">
          {userId || "No proporcionado"}
        </Text>
      </Text>

      <Pressable
        onPress={() => navigation.goBack()}
        className="bg-blue-600 py-3 px-4 rounded-xl active:bg-blue-700"
        accessibilityLabel="Volver a la pantalla anterior"
      >
        <Text className="text-white text-center font-semibold text-base">
          Volver
        </Text>
      </Pressable>
    </View>
  );
}

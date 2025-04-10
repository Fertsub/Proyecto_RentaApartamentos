// src/screens/ProfileScreen.tsx
import { View, Text, Button } from "react-native";
import { NativeStackScreenProps } from "@react-navigation/native-stack";
import { RootStackParamList } from "../navigation/AppNav";
import React from "react";


type Props = NativeStackScreenProps<RootStackParamList, "Profile">;

export default function ProfileScreen({ route, navigation }: Props) {
  const { userId } = route.params || {};

  return (
    <View className="flex-1 p-4">
      <Text className="text-lg">ID de Usuario: {userId || "No proporcionado"}</Text>
      <Button 
        title="Volver" 
        onPress={() => navigation.goBack()} 
      />
    </View>
  );
}
import React, { useState, useEffect } from "react";
import {
  View,
  Text,
  Pressable,
  ActivityIndicator,
  Alert,
} from "react-native";
import api from "../utils/axios"; // Asegúrate que esto apunte a tu instancia personalizada
import { NativeStackScreenProps } from "@react-navigation/native-stack";
import { RootStackParamList } from "../navigation/AppNav";

type Props = NativeStackScreenProps<RootStackParamList, "Profile">;

interface Persona {
  dni: string;
  nombre: string;
  apellido: string;
  telefono: string;
  correo: string;
}

interface UserProfile {
  idArrendatario: string;
  persona: Persona;
}

export default function ProfileScreen({ route, navigation }: Props) {
  const { userId } = route.params || {};
  const [profile, setProfile] = useState<UserProfile | null>(null);
  const [loading, setLoading] = useState<boolean>(true);

  useEffect(() => {
    if (!userId) {
      setLoading(false);
      return;
    }

    (async () => {
      try {
        const response = await api.get<UserProfile>(`/Arrendatario/${userId}`);
        setProfile(response.data);
      } catch (err) {
        console.error(err);
        Alert.alert("Error", "No se pudo cargar la información del usuario.");
      } finally {
        setLoading(false);
      }
    })();
  }, [userId]);

  if (loading) {
    return (
      <View className="flex-1 items-center justify-center bg-white">
        <ActivityIndicator size="large" color="#3b82f6" />
        <Text className="mt-4 text-gray-500">Cargando perfil...</Text>
      </View>
    );
  }

  if (!profile) {
    return (
      <View className="flex-1 items-center justify-center bg-white px-6">
        <Text className="text-base text-gray-700 mb-4">
          No se encontró información de usuario.
        </Text>
        <Pressable
          onPress={() => navigation.goBack()}
          className="bg-blue-600 py-3 px-4 rounded-xl active:bg-blue-700"
        >
          <Text className="text-white text-center font-semibold text-base">
            Volver
          </Text>
        </Pressable>
      </View>
    );
  }

  const { persona } = profile;

  return (
    <View className="flex-1 bg-white px-6 py-8">
      <Text className="text-2xl font-bold text-gray-900 mb-4">
        Hola, {persona.nombre}
      </Text>

      <View className="mb-6">
        <Text className="text-sm text-gray-500">Nombre completo</Text>
        <Text className="text-base font-medium text-gray-800">
          {persona.nombre} {persona.apellido}
        </Text>
      </View>

      <View className="mb-6">
        <Text className="text-sm text-gray-500">DNI</Text>
        <Text className="text-base font-medium text-gray-800">{persona.dni}</Text>
      </View>

      <View className="mb-6">
        <Text className="text-sm text-gray-500">Correo electrónico</Text>
        <Text className="text-base font-medium text-gray-800">
          {persona.correo}
        </Text>
      </View>

      <View className="mb-6">
        <Text className="text-sm text-gray-500">Teléfono</Text>
        <Text className="text-base font-medium text-gray-800">
          {persona.telefono}
        </Text>
      </View>

      <Pressable
        onPress={() => navigation.goBack()}
        className="mt-auto bg-blue-600 py-3 px-4 rounded-xl active:bg-blue-700"
        accessibilityLabel="Volver a la pantalla anterior"
      >
        <Text className="text-white text-center font-semibold text-base">
          Volver
        </Text>
      </Pressable>
    </View>
  );
}

import React, { useState } from "react";
import {
  View,
  Text,
  TextInput,
  Pressable,
  Alert,
  ActivityIndicator,
} from "react-native";
import { Picker } from "@react-native-picker/picker";
import api from "../utils/axios";

export default function QuejasScreen({ navigation }) {
  const [descripcion, setDescripcion] = useState("");
  const [apartamentoId, setApartamentoId] = useState("");
  const [tipoQueja, setTipoQueja] = useState("Ruido"); // Valor inicial
  const [enviando, setEnviando] = useState(false);

  const handleEnviarQueja = async () => {
    if (!descripcion || !apartamentoId) {
      Alert.alert("Campos requeridos", "Por favor llena todos los campos.");
      return;
    }

    setEnviando(true);

    try {
      const descripcionCompleta = `Tipo de queja: ${tipoQueja}. Descripción: ${descripcion}`;

      const payload = {
        apartamento: { idApartamento: parseInt(apartamentoId) },
        descripcion: descripcionCompleta,
        estado: false,
        fecha: new Date().toISOString(),
        idQS: 0,
        arrendatario: null,
      };

      await api.post("/Queja_Solicitud", payload);

      Alert.alert("Éxito", "La queja fue registrada correctamente.");
      setDescripcion("");
      setApartamentoId("");
      setTipoQueja("Ruido");
    } catch (error) {
      console.error(error);
      Alert.alert("Error", "No se pudo enviar la queja.");
    } finally {
      setEnviando(false);
    }
  };

  return (
    <View className="flex-1 bg-white px-6 py-8">
      <Text className="text-2xl font-bold text-gray-900 mb-6">
        Registrar una Queja
      </Text>

      <View className="mb-4">
        <Text className="text-sm text-gray-500 mb-1">ID del Apartamento</Text>
        <TextInput
          value={apartamentoId}
          onChangeText={setApartamentoId}
          keyboardType="numeric"
          placeholder="Ej. 101"
          className="border border-gray-300 rounded-lg px-4 py-2 text-base text-gray-800"
        />
      </View>

      <View className="mb-4">
        <Text className="text-sm text-gray-500 mb-1">Tipo de Queja</Text>
        <View className="border border-gray-300 rounded-lg">
          <Picker
            selectedValue={tipoQueja}
            onValueChange={(itemValue) => setTipoQueja(itemValue)}
            style={{ height: 50 }}
          >
            <Picker.Item label="Ruido" value="Ruido" />
            <Picker.Item label="Vecinos" value="Vecinos" />
            <Picker.Item label="Servicios" value="Servicios" />
            <Picker.Item label="Otros" value="Otros" />
          </Picker>
        </View>
      </View>

      <View className="mb-6">
        <Text className="text-sm text-gray-500 mb-1">Descripción</Text>
        <TextInput
          value={descripcion}
          onChangeText={setDescripcion}
          placeholder="Describe tu queja aquí..."
          multiline
          numberOfLines={4}
          className="border border-gray-300 rounded-lg px-4 py-2 text-base text-gray-800 h-32 text-start"
        />
      </View>

      <Pressable
        onPress={handleEnviarQueja}
        className="bg-blue-600 py-3 px-4 rounded-xl active:bg-blue-700"
        disabled={enviando}
      >
        {enviando ? (
          <ActivityIndicator color="#fff" />
        ) : (
          <Text className="text-white text-center font-semibold text-base">
            Enviar Queja
          </Text>
        )}
      </Pressable>
    </View>
  );
}

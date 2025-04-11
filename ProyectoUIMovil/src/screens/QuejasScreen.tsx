import React, { useState } from "react";
import {
  View,
  Text,
  Pressable,
  TextInput,
  ScrollView,
  ActivityIndicator,
} from "react-native";
import { Picker } from "@react-native-picker/picker";
import axios, { AxiosError } from "axios";
import { ArrowLeft, Send } from "lucide-react-native";
import { NativeStackScreenProps } from "@react-navigation/native-stack";
import { RootStackParamList } from "../navigation/AppNav";

type ComplaintCategory = "noise" | "security" | "cleanliness" | "neighbors" | "other";

interface ComplaintForm {
  subject: string;
  category: ComplaintCategory;
  description: string;
  photo: string | null;
}

type Props = NativeStackScreenProps<RootStackParamList, "Complaints">;

export default function ComplaintsPage({ navigation }: Props) {
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [formData, setFormData] = useState<ComplaintForm>({
    subject: "",
    category: "noise",
    description: "",
    photo: null,
  });

  const handleSubmit = async () => {
    setIsSubmitting(true);
    try {
      const response = await axios.post("https://tu-api.com/complaints", formData);

      if (response.data.success) {
        alert("¡Queja enviada con éxito!");
        setFormData({
          subject: "",
          category: "noise",
          description: "",
          photo: null,
        });
      }
    } catch (error) {
      const axiosError = error as AxiosError;
      alert(axiosError.message || "Error al enviar la queja");
      console.error(error);
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <ScrollView className="flex-1 bg-gray-50 px-4 py-6">
      {/* Header */}
      <View className="flex-row items-center mb-6">
        <Pressable onPress={() => navigation.goBack()} className="p-2">
          <ArrowLeft size={24} color="#000" />
        </Pressable>
        <Text className="text-xl font-bold ml-4 text-gray-900">Quejas</Text>
      </View>

      {/* Form Card */}
      <View className="bg-white p-5 rounded-2xl shadow-sm border border-gray-100">
        <Text className="text-lg font-semibold text-gray-800 mb-1">Enviar una queja</Text>
        <Text className="text-sm text-gray-500 mb-4">
          Complete el formulario para enviar una queja
        </Text>

        {/* Asunto */}
        <View className="mb-4">
          <Text className="text-sm font-medium text-gray-700 mb-1">Asunto</Text>
          <TextInput
            placeholder="Asunto de la queja"
            value={formData.subject}
            onChangeText={(text) => setFormData({ ...formData, subject: text })}
            className="border border-gray-300 rounded-lg p-3 bg-white text-gray-900"
          />
        </View>

        {/* Categoría */}
        <View className="mb-4">
          <Text className="text-sm font-medium text-gray-700 mb-1">Categoría</Text>
          <View className="border border-gray-300 rounded-lg overflow-hidden bg-white">
            <Picker
              selectedValue={formData.category}
              onValueChange={(value: ComplaintCategory) =>
                setFormData({ ...formData, category: value })
              }
            >
              <Picker.Item label="Ruido" value="noise" />
              <Picker.Item label="Seguridad" value="security" />
              <Picker.Item label="Limpieza" value="cleanliness" />
              <Picker.Item label="Vecinos" value="neighbors" />
              <Picker.Item label="Otro" value="other" />
            </Picker>
          </View>
        </View>

        {/* Descripción */}
        <View className="mb-6">
          <Text className="text-sm font-medium text-gray-700 mb-1">Descripción</Text>
          <TextInput
            placeholder="Describa su queja en detalle"
            multiline
            numberOfLines={5}
            value={formData.description}
            onChangeText={(text) => setFormData({ ...formData, description: text })}
            className="border border-gray-300 rounded-lg p-3 bg-white text-gray-900 h-32 text-sm"
          />
        </View>

        {/* Botón */}
        <Pressable
          onPress={handleSubmit}
          disabled={isSubmitting}
          className="bg-blue-600 py-3 rounded-xl flex-row justify-center items-center active:bg-blue-700"
        >
          {isSubmitting ? (
            <ActivityIndicator color="#fff" />
          ) : (
            <>
              <Send size={18} color="#fff" className="mr-2" />
              <Text className="text-white text-base font-medium ml-2">Enviar queja</Text>
            </>
          )}
        </Pressable>
      </View>
    </ScrollView>
  );
}

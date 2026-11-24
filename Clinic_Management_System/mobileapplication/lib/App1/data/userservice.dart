import 'dart:convert';
import 'package:http/http.dart' as http;

import 'conniction.dart';


class UserService {



  Future<Map<String, dynamic>?> login(String username, String password) async {

    final response = await http.post(
      Uri.parse(ConectionString.login),

      headers: {
        'Content-Type': 'application/json',
      },

      body: json.encode({
        'username': username,
        'password': password,
      }),
    );

    if (response.statusCode == 200) {
      return json.decode(response.body);
    } else if (response.statusCode == 401) {
      return null;
    } else {
      throw Exception('Failed to login');
    }
  }





  Future<Map<String, dynamic>?> register(
      String firstName,
      String lastName,
      String email,
      String username,
      String password,
      String birthDate,
      String gender,
      String phone
      ) async {

    final response = await http.post(
      Uri.parse(ConectionString.register),

      headers: {
        'Content-Type': 'application/json',
      },

      body: json.encode({
        'firstName': firstName,
        'lastName': lastName,
        'email': email,
        'username': username,
        'password': password,
        'birthDate': birthDate,
        'gender': gender,
        'phone': phone,
      }),
    );

    if (response.statusCode == 200) {
      return json.decode(response.body);
    } else if (response.statusCode == 400) {
      return null;
    } else {
      throw Exception('Registration failed: ${response.body}');
    }
  }
}
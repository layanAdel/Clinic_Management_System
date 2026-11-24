import 'package:flutter/material.dart';

import '../../App2/data/userservice.dart';

class Userprovider extends ChangeNotifier {

  final UserService userservice = UserService();

  Map<String, dynamic>? user;

  bool isLoading =false;

  String? error;

Future<bool> register (
      String firstName,
      String lastName,
      String email,
      String username,
      String password,
      String birthDate,
      String gender,
      String phone,
      ) async {

    isLoading = true;
    notifyListeners();

    try {

      final result =
          await userservice.register(
             firstName,
             lastName,
             email,
             username,
             password,
             birthDate,
             gender,
             phone,
          );
      return result !=null;

    } finally{

      isLoading = false;
      notifyListeners();

    }
  }

  //Login

  Future<bool> login (
      String username,
      String password,
      ) async {

    isLoading =true;
    error = null;
    notifyListeners();

    try {
      user =await userservice.login(username,password);

      if (user == null ){
        error = 'Invalid username or password';
        return false;
      }

      return true;
    } catch (e){
      error = 'Failed to connect to server';
      return false;

    } finally {
      isLoading =false;
      notifyListeners();
    }
  }


  // user data
  String get username => user?['username']?.toString() ?? '';

  String get email =>user?['email']?.toString()?? '';

  int get userId => (user?['id'] as num?)?.toInt() ?? 0;

  int get patientId => (user?['patientId'] as num?)?.toInt() ?? 0;

  bool get isLoggedIn => user != null;


  // Logout

void logout(){
  user = null;
  error = null;
  notifyListeners();
  }
}





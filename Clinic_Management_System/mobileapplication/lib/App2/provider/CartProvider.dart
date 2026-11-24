import 'package:flutter/material.dart';
import '../data/cartService.dart';

class CartProvider extends ChangeNotifier {

  final Cartservice cartservice = Cartservice();

  List<dynamic> cart = [];

  bool isLoading = false;

  String? error;

  // Get Cart
  Future<void> getCart(int patientId) async {

    isLoading = true;
    error = null;
    notifyListeners();

    try {
      cart = await cartservice.getCart(patientId);
    }
    catch (e) {
      error = 'Failed to load cart';
    }
    finally {
      isLoading = false;
      notifyListeners();
    }
  }

  // Add To Cart
  Future<bool> addToCart(int patientId, int appointmentId) async {

    isLoading = true;
    error = null;
    notifyListeners();

    try {
      await cartservice.addToCart(patientId, appointmentId);

      await getCart(patientId);

      return true;
    }
    catch (e) {
      error = 'Failed to add appointment';
      return false;
    }
    finally {
      isLoading = false;
      notifyListeners();
    }
  }

  // Remove From Cart
  Future<bool> removeFromCart(int patientId, int appointmentId) async {

    isLoading = true;
    error = null;
    notifyListeners();

    try {
      await cartservice.removeFromCart(patientId, appointmentId);

      await getCart(patientId);

      return true;
    }
    catch (e) {
      error = 'Failed to remove appointment';
      return false;
    }
    finally {
      isLoading = false;
      notifyListeners();
    }
  }
}
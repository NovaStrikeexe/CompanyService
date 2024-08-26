import requests

BASE_URL = "http://localhost:5000/api/Companies"

def test_get_all_companies():
    response = requests.get(BASE_URL)
    assert response.status_code == 200
    data = response.json()
    assert isinstance(data, list)

def test_get_company_by_id():
    response = requests.get(f"{BASE_URL}/1")
    assert response.status_code == 200
    data = response.json()
    assert "id" in data
    assert data["id"] == 1

def test_add_company():
    new_company = {"name": "Test Company"}
    response = requests.post(BASE_URL, json=new_company)
    assert response.status_code == 201
    data = response.json()
    assert data["name"] == "Test Company"
    assert "id" in data

def test_update_company():
    updated_company = {"id": 1, "name": "Updated Company"}
    response = requests.put(f"{BASE_URL}/1", json=updated_company)
    assert response.status_code == 204

def test_delete_company():
    response = requests.delete(f"{BASE_URL}/1")
    assert response.status_code == 204

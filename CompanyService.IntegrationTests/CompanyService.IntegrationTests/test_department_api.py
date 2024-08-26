import requests

BASE_URL = "http://localhost:8080/api/Departments"

def test_get_all_departments():
    response = requests.get(BASE_URL)
    assert response.status_code == 200
    data = response.json()
    assert isinstance(data, list)

def test_get_department_by_id():
    response = requests.get(f"{BASE_URL}/1")
    assert response.status_code == 200
    data = response.json()
    assert "id" in data
    assert data["id"] == 1

def test_add_department():
    new_department = {"name": "Test Department", "companyId": 1}
    response = requests.post(BASE_URL, json=new_department)
    assert response.status_code == 201
    data = response.json()
    assert data["name"] == "Test Department"
    assert "id" in data

def test_update_department():
    updated_department = {"id": 1, "name": "Updated Department", "companyId": 1}
    response = requests.put(f"{BASE_URL}/1", json=updated_department)
    assert response.status_code == 204

def test_delete_department():
    response = requests.delete(f"{BASE_URL}/1")
    assert response.status_code == 204

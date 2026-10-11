import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { PetService } from '../../core/services/pet.service';

@Component({
  selector: 'app-pets',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule
  ],
  templateUrl: './pets.html',
  styleUrl: './pets.css'
})
export class Pets implements OnInit {

  pets: any[] = [];

  today: string = new Date()
    .toISOString()
    .split('T')[0];

  showPets = false;

  customerId = Number(
    localStorage.getItem('customerId')
  );

  selectedCoverage: any = null;

  editPetModel = {
    petId: 0,
    petName: '',
    species: '',
    breed: '',
    dateOfBirth: ''
  };

  newPet = {
    petName: '',
    species: '',
    breed: '',
    dateOfBirth: ''
  };

  constructor(
    private petService: PetService
  ) { }

  ngOnInit(): void {
  }

  viewPets(): void {

    this.showPets = false;

    this.petService
      .getPets(this.customerId)
      .subscribe({

        next: (data: any[]) => {

  this.pets = data.map(pet => ({
    ...pet,
    isActive: true
  }));

  this.showPets = true;

},

        error: (err: any) => {

          console.error(
            'Load Pets Error',
            err
          );

        }

      });

  }

  addPet(): void {

    if (
      !this.newPet.petName.trim() ||
      !this.newPet.species.trim() ||
      !this.newPet.breed.trim() ||
      !this.newPet.dateOfBirth
    ) {

      alert(
        'All pet fields are required.'
      );

      return;

    }

    const petDob = new Date(
      this.newPet.dateOfBirth
    );

    const today = new Date();

    today.setHours(
      0,
      0,
      0,
      0
    );

    if (petDob > today) {

      alert(
        'Pet Date of Birth cannot be in the future.'
      );

      return;

    }

    this.newPet.dateOfBirth =
      this.newPet.dateOfBirth.split('T')[0];

    this.petService
      .addPet(
        this.customerId,
        this.newPet
      )
      .subscribe({

        next: () => {

          alert(
            'Pet Added Successfully'
          );

          this.newPet = {
            petName: '',
            species: '',
            breed: '',
            dateOfBirth: ''
          };

          this.viewPets();

        },

        error: (err: any) => {

          console.error(
            'Add Pet Error',
            err
          );

        }

      });

  }

  editPet(
    pet: any
  ): void {

    let dob =
      pet.dateOfBirth?.split('T')[0] || '';

    const selectedDob =
      new Date(dob);

    const currentDate =
      new Date();

    currentDate.setHours(
      0,
      0,
      0,
      0
    );

    if (selectedDob > currentDate) {

      dob = this.today;

    }

    this.editPetModel = {

      petId: pet.petId,

      petName: pet.petName,

      species: pet.species,

      breed: pet.breed,

      dateOfBirth: dob

    };

  }

  updatePet(): void {

    if (
      !this.editPetModel.petName.trim() ||
      !this.editPetModel.species.trim() ||
      !this.editPetModel.breed.trim() ||
      !this.editPetModel.dateOfBirth
    ) {

      alert(
        'All pet fields are required.'
      );

      return;

    }

    const petDob =
      new Date(
        this.editPetModel.dateOfBirth
      );

    const today =
      new Date();

    today.setHours(
      0,
      0,
      0,
      0
    );

    if (petDob > today) {

      alert(
        'Pet Date of Birth cannot be in the future.'
      );

      return;

    }

    this.editPetModel.dateOfBirth =
      this.editPetModel.dateOfBirth.split('T')[0];

    this.petService
      .updatePet(
        this.customerId,
        this.editPetModel.petId,
        this.editPetModel
      )
      .subscribe({

        next: () => {

          alert(
            'Pet Updated Successfully'
          );

          this.viewPets();

          this.cancelEdit();

        },

        error: (err: any) => {

          console.error(
            'Update Pet Error',
            err
          );

        }

      });

  }

  cancelEdit(): void {

    this.editPetModel = {

      petId: 0,

      petName: '',

      species: '',

      breed: '',

      dateOfBirth: ''

    };

  }

  deactivatePet(
  pet: any
): void {

  if (
    !confirm(
      'Are you sure you want to deactivate this pet?'
    )
  ) {
    return;
  }

  pet.isActive = false;

}

  

  viewCoverage(
    pet: any
  ): void {

    this.selectedCoverage = pet;

  }

  closeCoverage(): void {

    this.selectedCoverage = null;

  }

  calculateAge(
    dateOfBirth: string
  ): number {

    const dob =
      new Date(dateOfBirth);

    const today =
      new Date();

    let age =
      today.getFullYear() -
      dob.getFullYear();

    const monthDiff =
      today.getMonth() -
      dob.getMonth();

    if (
      monthDiff < 0 ||
      (
        monthDiff === 0 &&
        today.getDate() < dob.getDate()
      )
    ) {
      age--;
    }

    return age;

  }

}
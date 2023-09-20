﻿using Microsoft.AspNetCore.Mvc;
using PhoneBookApi.Dto;
using PhoneBookApi.Models;
using PhoneBookApi.Services;

namespace PhoneBookApi.Controllers
{
    [Route("phonebook")]
    [ApiController]
    public class PhoneBookController : ControllerBase
    {
        private readonly PhoneBookService _phoneBookService;
        public PhoneBookController(PhoneBookService phoneBookService) => _phoneBookService = phoneBookService;

        [HttpPost]
        public async Task<ActionResult<PhoneNumber>> CreatePhoneNumber(CreatePhoneNumberDto createPhoneNumberDto)
        {
            var created = await _phoneBookService.CreatePhoneNumber(createPhoneNumberDto);

            return Ok(created);
        }

        [HttpGet]
        public async Task<List<PhoneNumber>> GetAllPhoneNumbers() => await _phoneBookService.GetAllPhoneNumbers();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<PhoneNumber>> GetPhoneNumberById(string id)
        {
            var phoneNumber = await _phoneBookService.GetPhoneNumberById(id);

            if (phoneNumber is null) return NotFound();

            return phoneNumber;
        }

        [HttpPut("{id:length(24)}")]
        public async Task<ActionResult<PhoneNumber>> UpdatePhoneNumber(string id, UpdatePhoneNumberDto updatePhoneNumberDto)
        {
            var phoneNumber = await _phoneBookService.GetPhoneNumberById(id);

            if (phoneNumber is null) return NotFound();

            var updated = await _phoneBookService.UpdatePhoneNumber(id, updatePhoneNumberDto);

            return Ok(updated);
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<ActionResult<PhoneNumber>> Delete(string id)
        {
            var phoneNumber = await _phoneBookService.GetPhoneNumberById(id);

            if (phoneNumber is null) return NotFound();

            var deleted = await _phoneBookService.DeletePhoneNumber(id);

            return Ok(deleted);
        }
    }
}

using DNABank.API.Models;
using DNABank.Application.Features.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace DNABank.API.Controllers.V1
{
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class AccountsController(IAccountService accountService) : ControllerBase
    {
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var account = await accountService.GetAccountById(id);
            return Ok(account);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllAccounts()
        {
            var accounts = await accountService.GetAllAccounts();
            return Ok(accounts);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAccount([FromBody] AccountRequest accountRequest)
        {
            var createdAccount = await accountService.CreateAccount(accountRequest.FullName, accountRequest.AccountType);
            return CreatedAtAction(nameof(GetById), new { id = createdAccount.Id }, createdAccount);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAccountStatus([FromRoute] Guid id, bool isFrozen)
        {
            var result = await accountService.UpdateAccountStatus(id, isFrozen);
            return Ok(result);
        }
    }
}

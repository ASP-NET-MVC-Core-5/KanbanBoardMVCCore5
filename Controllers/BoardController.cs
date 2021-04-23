using KanbanBoardMVCCore5.Data;
using KanbanBoardMVCCore5.Models;
using KanbanBoardMVCCore5.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace KanbanBoardMVCCore5.Controllers
{
    public class BoardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IBoard _board;

        public BoardController(ApplicationDbContext context, IBoard board)
        {
            _context = context;
            _board = board;
        }

        // GET: BoardController
        public async Task<IActionResult> Index()
        {    
            
            return View(await _context.Boards.ToListAsync());
        }

        // GET: BoardController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            return View();
        }

        // GET: BoardController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BoardController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Board board)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    board.UserEmail = User.Identity.Name;
                    _context.Add(board);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(board);
            }
            catch
            {
                return View();
            }
        }

        // GET: BoardController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var board = await _context.Boards.FindAsync(id);
            if (board == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", board.UserId);
            return View(board);
        }

        // POST: BoardController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  Board board)
        {
            if (id != board.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    board.UserEmail = User.Identity.Name;
                    _context.Update(board);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserStoryExists(board.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", board.UserId);
            return View(board);
        }



        // GET: BoardController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BoardController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        private bool UserStoryExists(int id)
        {
            return _context.Boards.Any(e => e.Id == id);
        }
        [HttpPost]
        public async Task<IActionResult> Forward(int id)
        {
            Board story = _context.Boards.FirstOrDefault(i => i.Id == id);
            _board.Forward(story);
            _context.Update(story);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> MoveUserStoryBackward(int id)
        {
            Board story = _context.Boards.FirstOrDefault(i => i.Id == id);
            _board.Backward(story);
            _context.Update(story);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));


        }
    }
}

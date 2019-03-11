using System;
using Microsoft.AspNetCore.Mvc;

namespace WebApp
{
    [Route("garbage-collection")]
    [ApiController]
    public class GarbageCollectionController
    {
        [HttpPost("invoke")]
        public void Invoke()
        {
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true, true);
        }
    }
}
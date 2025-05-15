using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using static GM_Global;

public static class TargetingHelper
{
    // 等待并返回具有指定组件的 GameObject 引用
    public static async Task<GameObject> WaitForTargetWithComponentAsync<T>(PlayerState state, GM_Global GM_Global) where T : Component
    {
        // 使用 TaskCompletionSource 来创建一个任务
        var tcs = new TaskCompletionSource<GameObject>();

        // 启动协程来执行任务
        await Task.Yield(); // 需要在 async 方法中等待一帧以启动协程

        // 启动协程
        GM_Global.StartCoroutine(WaitForTargetWithComponentInternal<T>(state, GM_Global, tcs));

        // 返回 Task，async 方法会在此处等待
        return await tcs.Task;
    }

    // 内部协程，用于等待目标选择并完成任务
    private static IEnumerator WaitForTargetWithComponentInternal<T>(PlayerState state, GM_Global GM_Global, TaskCompletionSource<GameObject> tcs) where T : Component
    {
        GM_Global.Target = null;
        GM_Global.CurrentPlayerState = state;

        // 等待直到玩家选择目标
        while (GM_Global.Target == null)
        {
            Debug.Log("等待玩家点击目标...");
            yield return null;
        }

        GM_Global.CurrentPlayerState = PlayerState.Idle;

        // 如果目标有指定组件，完成任务并返回目标的 GameObject，否则返回 null
        if (GM_Global.Target.TryGetComponent<T>(out var component))
        {
            tcs.SetResult(GM_Global.Target);  // 目标包含组件，返回目标
        }
        else
        {
            tcs.SetResult(null);  // 目标不包含组件，返回 null
        }
    }

}
